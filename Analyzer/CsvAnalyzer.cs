using System.Runtime.CompilerServices;
using Lib.Analyzer.Interfaces;

namespace Lib.Analyzer;

public delegate void CsvAnalysisStartedEventHandler();
public class CsvAnalyzer : IAnalyzer
{

    public static event CsvAnalysisStartedEventHandler CsvAnalysisStartedEvent;
    public event AnalysisStepProgressedEventHandler AnalysisProgressedEvent;
    
    public async Task<string> RunAnalysisAsync((DateTime dateStart, DateTime dateFinish) dates)
    {
        CsvAnalysisStartedEvent.Invoke();
        Thread.Sleep(10000);
        return "csv";
    }
}