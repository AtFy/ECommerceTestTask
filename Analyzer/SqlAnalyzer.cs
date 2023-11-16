using Lib.Analyzer.Interfaces;

namespace Lib.Analyzer;

public class SqlAnalyzer : IAnalyzer
{
    public string RunAnalysis()
    {
        return "sql";
    }
}