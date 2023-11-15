using Application.Interfaces;
using Lib.Analyzer.Interfaces;

namespace Application;

public delegate void AnalysisCompletedEventHandler(string result);

public delegate void ProgressedEventHandler(float progress);
public class LogicWorker : ILogicWorker
{
    public LogicWorker(IDbController dbController)
    {
        _dbController = dbController;
    }

    public static event AnalysisCompletedEventHandler AnalysisCompletedEvent;

    public static event ProgressedEventHandler ProgressedEvent;
    
    public bool IsRunning { get; private set; }

    public float Progress { get; private set; } = 0f;

    private void IncreaseProgress() => Progress += 0.25f;

    private IDbController _dbController;
    
    public async Task RunSqlAnalysisAsync()
    {
        IsRunning = true;
        var result = await Task.Run(RunSqlAnalysis);
        IsRunning = false;
        Progress = 0f;
        
        AnalysisCompletedEvent.Invoke(result);
    }
    
    public async Task RunNoSqlAnalysisAsync()
    {
        IsRunning = true;
        var result = await Task.Run(RunNoSqlAnalysis);
        IsRunning = false;
        Progress = 0f;
        
        AnalysisCompletedEvent.Invoke(result);
    }
    
    private string RunSqlAnalysis()
    {
        Thread.Sleep(2000);
        IncreaseProgress();
        ProgressedEvent.Invoke(Progress);
        Thread.Sleep(1000);
        IncreaseProgress();
        ProgressedEvent.Invoke(Progress);
        Thread.Sleep(2000);
        IncreaseProgress();
        ProgressedEvent.Invoke(Progress);
        return "123";
    }

    private string RunNoSqlAnalysis()
    {
        Thread.Sleep(5000);
        return "423123";
    }
}