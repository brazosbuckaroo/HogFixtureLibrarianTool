namespace HogFixtureLibrarianTool.Models.Types;

public class SqlLiteManager : IDbManager
{
    private readonly string _fileDirectory = Path.Combine(AppContext.BaseDirectory, "Databases");

    private const string FileName = "FixtureBuilderFunctions.sqlite";

    public SqlLiteManager()
    {
        ConnectionString = new SQLiteConnectionStringBuilder
        {
            DataSource = Path.Combine(_fileDirectory, FileName),
            Version = 3
        };

        if (!Directory.Exists(_fileDirectory)) Directory.CreateDirectory(_fileDirectory);

        CreateDbFile();
    }

    public SQLiteConnectionStringBuilder ConnectionString { get; }

    public async Task<List<string>> GetTableAsync(string tableName, CancellationToken cancellation = default)
    {
        var data = new List<string>();

        await using var connection = new SQLiteConnection(ConnectionString.ConnectionString);
        await connection.OpenAsync(cancellation);

        await using var command = connection.CreateCommand();
        command.CommandText = @"SELECT NAME FROM %tableName%"
            .Replace("%tableName%", tableName);

        await command.PrepareAsync(cancellation);

        await using var transaction = connection.BeginTransaction();
        command.Transaction = transaction;
        command.Connection = connection;

        await using var reader = command.ExecuteReader();
        
        while (await reader.ReadAsync(cancellation))
            data.Add(await reader.GetFieldValueAsync<string>(0, cancellation));

        return data;
    }

    private void CreateDbFile()
    {
        var filePath = Path.Combine(_fileDirectory, FileName);

        if (File.Exists(filePath)) return;
        
        SQLiteConnection.CreateFile(filePath);

        // make tables
        using var connection = new SQLiteConnection(ConnectionString.ConnectionString);
        
        connection.Open();

        using (var command = connection.CreateCommand())
        {
            using (var transaction = connection.BeginTransaction())
            {
                command.Transaction = transaction;
                command.Connection = connection;
                command.CommandText = @"CREATE TABLE IF NOT EXISTS FUNCTIONS(ID INTEGER PRIMARY KEY ASC, 
                                                                                     NAME TEXT)";

                command.Prepare();
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE IF NOT EXISTS FEATURES(ID INTEGER PRIMARY KEY ASC,
                                                                                    NAME TEXT)";

                command.ExecuteNonQuery();
                transaction.Commit();
            }
        }

        CreateInitialTableData();
    }

    private void CreateInitialTableData()
    {
        var functions = new List<string>();
        var functionName = string.Empty;

        // add all macros
        for (var i = 0; i < 9; i++)
        {
            if (i == 0)
            {
                functionName = "Macro";

                i++; // skip 1
            }
            else
            {
                functionName = $"Macro {i}";
            }

            functions.Add(functionName);
        }

        // add all beam fx's
        for (var i = 0; i < 10; i++)
        {
            if (i == 0)
            {
                functionName = "Beam Fx";

                i++; // skip 1
            }
            else
            {
                functionName = $"Beam Fx {i}";
            }

            functions.Add(functionName);
        }

        // add all colour's
        for (var i = 0; i < 3; i++)
        {
            if (i == 0)
            {
                functionName = "Colour";

                i++; // skip 1
            }
            else
            {
                functionName = $"Colour {i}";
            }

            functions.Add(functionName);
        }

        functions.Add("Control Mode");
        functions.Add("Colour Fx");

        // create default function values
        InsertData("FUNCTIONS", functions);

        functions.Clear();

        var features = new List<string>();

        features.Add("Continuous");
        features.Add("Variable");
        features.Add("Direct");
        features.Add("Discrete");

        // create default features values
        InsertData("FEATURES", features);

        features.Clear();
    }

    private void InsertData(string tableName, List<string> data)
    {
        using var connection = new SQLiteConnection(ConnectionString.ConnectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = @"INSERT INTO %tableName%(NAME)
                                        VALUES ($data)"
            .Replace("%tableName%", tableName);

        using var transaction = connection.BeginTransaction();
        command.Transaction = transaction;
        command.Connection = connection;

        foreach (var value in data)
        {
            command.Parameters.AddWithValue("$data", value);

            command.Prepare();
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }

        transaction.Commit();
    }
}