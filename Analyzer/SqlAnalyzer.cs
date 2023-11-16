using Lib.Analyzer.Interfaces;

namespace Lib.Analyzer;

public class SqlAnalyzer : IAnalyzer
{
    public string RunAnalysis((DateOnly dateStart, DateOnly dateFinish) dates)
    {
        Thread.Sleep(1000);
        return "sql";
    }
}