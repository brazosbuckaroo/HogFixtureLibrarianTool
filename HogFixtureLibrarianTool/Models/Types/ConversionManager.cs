namespace HogFixtureLibrarianTool.Models.Types;

public class ConversionManager : IConvertor
{
    public string ConvertArabNumeralToString(int number)
    {
        var output = string.Empty;

        switch (number)
        {
            case > 0 and < 10:
                output += ConvertOnesPlaceToString(number);
                break;
            case >= 10 and < 20:
            // we want to fall through because zero and the teens are our 
            // special cases. Hence, no break needed here.
            // -DTL
            case 0:
                output += ConvertSpecialCasesToString(number); // they're crazy!!!!!!
                break;
            default:
            {
                var numAsStr = new StringBuilder(number.ToString());

                if (numAsStr.Length == 3)
                {
                    output += ConvertOnesPlaceToString(int.Parse(numAsStr[0].ToString()));
                    output += " hundred ";
                }

                // allow for expandability to support more than three-digit numbers
                var tensPlaceValue = numAsStr.Length - 2;
                var onesPlaceValue = numAsStr.Length - 1;
                var tensDigit = numAsStr[tensPlaceValue];
                var onesDigit = numAsStr[onesPlaceValue];
                var tempStr = string.Concat(tensDigit.ToString(), onesDigit.ToString());

                if (!int.TryParse(tempStr, out var temp))
                    throw new InvalidCastException("Could not combine the tens and ones digits to make a number.");

                if (temp is > 9 and < 20)
                    output += ConvertSpecialCasesToString(temp);
                else
                    for (var i = tensPlaceValue; i < numAsStr.Length; i++)
                        if (i == tensPlaceValue && int.TryParse(numAsStr[i].ToString(), out var tensNumber))
                        {
                            output += ConvertTensPlaceToString(tensNumber);
                        }
                        else if (i == onesPlaceValue && int.TryParse(numAsStr[i].ToString(), out var onesNumber))
                        {
                            if (onesNumber != 0 && numAsStr[tensPlaceValue] != '0') output += "-";

                            output += ConvertOnesPlaceToString(onesNumber);
                        }

                break;
            }
        }

        return output.TrimEnd();
    }

    private string ConvertOnesPlaceToString(int number)
    {
        var output = string.Empty;

        switch (number)
        {
            case 1:
                output += "one";

                break;
            case 2:
                output += "two";

                break;
            case 3:
                output += "three";

                break;
            case 4:
                output += "four";

                break;
            case 5:
                output += "five";

                break;
            case 6:
                output += "six";

                break;
            case 7:
                output += "seven";

                break;
            case 8:
                output += "eight";

                break;
            case 9:
                output += "nine";

                break;
        }

        return output;
    }

    private string ConvertTensPlaceToString(int number)
    {
        var output = string.Empty;

        switch (number)
        {
            case 2:
                output += "twenty";

                break;
            case 3:
                output += "thirty";

                break;
            case 4:
                output += "forty";

                break;
            case 5:
                output += "fifty";

                break;
            case 6:
                output += "sixty";

                break;
            case 7:
                output += "seventy";

                break;
            case 8:
                output += "eighty";

                break;
            case 9:
                output += "ninety";

                break;
        }

        return output;
    }

    private string ConvertSpecialCasesToString(int number)
    {
        var output = string.Empty;

        switch (number)
        {
            case 0:
                output += "zero";

                break;
            case 10:
                output += "ten";

                break;
            case 11:
                output += "eleven";

                break;
            case 12:
                output += "twelve";

                break;
            case 13:
                output += "thirteen";

                break;
            case 14:
                output += "fourteen";

                break;
            case 15:
                output += "fifteen";

                break;
            case 16:
                output += "sixteen";

                break;
            case 17:
                output += "seventeen";

                break;
            case 18:
                output += "eighteen";

                break;
            case 19:
                output += "nineteen";

                break;
        }

        return output;
    }
}