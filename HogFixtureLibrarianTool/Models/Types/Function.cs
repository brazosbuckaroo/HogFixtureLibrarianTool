namespace HogFixtureLibrarianTool.Models.Types;

public record Function(
    string Name,
    string Kind,
    int Id,
    string Family,
    string SubName,
    string Group,
    string GroupBuddy,
    LinkedList<Feature> Features,
    LinkedList<Function>? Mutexes);