using Lib.Analyzer;
using Lib.Analyzer.Interfaces;
using Lib.DbController;

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
    private static Dictionary<Commands, string> _commandsInterpritation = new Dictionary<Commands, string>()
    {
        { Commands.SqlAnalysis, "1" },
        { Commands.CsvAnalysis, "2" },
        { Commands.Stop, "stop" }
    };

    private static Dictionary<string, IAnalyzer> _commandAnalyzers = new Dictionary<string, IAnalyzer>()
    {
        { _commandsInterpritation[Commands.SqlAnalysis], 
            new SqlAnalyzer(new ResilientDbController(new DbController())) },
        { _commandsInterpritation[Commands.CsvAnalysis], new CsvAnalyzer() }
    };

    public static string GetCommandEnterpritation(Commands command) => _commandsInterpritation[command];

    public static IAnalyzer GetAnalyzer(string command) => _commandAnalyzers[command];

    public static bool CheckIfCommand(string input) => _commandsInterpritation.Values.Contains(input);
}