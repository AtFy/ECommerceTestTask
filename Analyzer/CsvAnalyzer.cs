using Lib.Analyzer.Interfaces;

namespace Lib.Analyzer;

public class CsvAnalyzer : IAnalyzer
{
    public string RunAnalysis()
    {
        return "csv";
    }
}