using System.Text.RegularExpressions;

namespace Application;

public static class Validator
{
    public static bool CheckIfInputFormatCorrect(string input) => 
        input == CommandInterpritator.GetCommandEnterpritation(Commands.Stop) ||
        Regex.IsMatch(input, "^[\\w]+? [|] [\\d]{2}[.][\\d]{2}[.][\\d]{4}[-][\\d]{2}[.][\\d]{2}[.][\\d]{4}$");
}