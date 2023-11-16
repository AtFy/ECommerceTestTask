using Lib.Analyzer.Interfaces;

namespace Lib.Analyzer;

public class CsvAnalyzer : IAnalyzer
{
    public string RunAnalysis((DateOnly dateStart, DateOnly dateFinish) dates)
    {
        return "csv";
    }
}