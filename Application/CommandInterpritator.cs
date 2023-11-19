using Lib.Analyzer;
using Lib.Analyzer.Interfaces;
using Lib.DbController;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace Application;

public enum Commands
{
    SqlAnalysis,
    CsvAnalysis,
    Stop
}

public static class CommandInterpritator
{
    // This is the only place, where you need to change commands' string representation.
    private static  Dictionary<Commands, string> _commandsInterpritation = new()
    {
        { Commands.SqlAnalysis, "1" },
        { Commands.CsvAnalysis, "2" },
        { Commands.Stop, "stop" }
    };

    public static string GetCommandEnterpritation(Commands command) => _commandsInterpritation[command];

    public static IAnalyzer GetAnalyzer(string command)
    {
        if (command == _commandsInterpritation[Commands.SqlAnalysis])
        {
            return new SqlAnalyzer(new ResilientDbController(new DbController()));
        }

        if (command == _commandsInterpritation[Commands.CsvAnalysis])
        {
            return new CsvAnalyzer();
        }

        throw new Exception("Something went wrong when getting an analyzer.");
    }
    

    public static bool CheckIfCommand(string input) => _commandsInterpritation.Values.Contains(input);
}