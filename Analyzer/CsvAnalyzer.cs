using System.Runtime.CompilerServices;
using Lib.Analyzer.Interfaces;

namespace Lib.Analyzer;

public class CsvAnalyzer : IAnalyzer
{
    public event AnalysisStepProgressedEventHandler AnalysisProgressedEvent;
    
    public async Task<string> RunAnalysisAsync((DateTime dateStart, DateTime dateFinish) dates)
    {
        return "csv";
    }
}