namespace Application;

public enum Commands
{
    SqlAnalysis,
    NoSqlAnalysis,
    Stop
}

public static class CommandInterpritator
{
    private static Dictionary<Commands, string> _commandsInterpritation = new Dictionary<Commands, string>()
    {
        { Commands.SqlAnalysis, "1" },
        { Commands.NoSqlAnalysis, "2" },
        { Commands.Stop, "stop" }
    };

    public static string GetCommandEnterpritation(Commands command) => _commandsInterpritation[command];
}