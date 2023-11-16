using Application.Interfaces;
using Lib.Analyzer;
using Lib.Analyzer.Interfaces;

namespace Application;

public delegate void AnalysisCompletedEventHandler(string result);

public delegate void ProgressedEventHandler(float progress);
public class LogicWorker : ILogicWorker
{
    public LogicWorker()
    {

    }

    public static event AnalysisCompletedEventHandler AnalysisCompletedEvent;

    public static event ProgressedEventHandler ProgressedEvent;
    
    public bool IsRunning { get; private set; }

    public float Progress { get; private set; } = 0f;

    private void IncreaseProgress() => Progress += 0.25f;
    
    public async Task RunAnalysisAsync(IAnalyzer analyzer)
    {
        IsRunning = true;
        var result = await Task.Run(() => RunAnalysis(analyzer));
        IsRunning = false;
        Progress = 0f;
        
        AnalysisCompletedEvent.Invoke(result);
    }
    
    private string RunAnalysis(IAnalyzer analyzer)
    {
        return analyzer.RunAnalysis();
    }
}