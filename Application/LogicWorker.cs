﻿
using Application.Interfaces;

using Lib.Analyzer.Interfaces;

namespace Application;

public delegate void AnalysisCompletedEventHandler(string result,(DateTime dateStart, DateTime dateFinish) dates);

public delegate void ProgressedEventHandler(float progress);
public class LogicWorker : ILogicWorker
{
    public static event AnalysisCompletedEventHandler AnalysisCompletedEvent;

    public static event ProgressedEventHandler ProgressedEvent;
    
    public bool IsRunning { get; private set; }

    public float Progress { get; private set; } = 0f;

    private object Locker = new();
    
    private void IncreaseProgress(float stepSize)
    {
        lock (Locker)
        {
            Progress += stepSize;
            ProgressedEvent.Invoke(Progress);
        }
    }

    public async Task RunAnalysisAsync(IAnalyzer analyzer, (DateTime dateStart, DateTime dateFinish) dates)
    {
        IsRunning = true;
        analyzer.AnalysisProgressedEvent += IncreaseProgress;
        
        var result = await Task.Run(() => analyzer.RunAnalysisAsync(dates));
        
        IsRunning = false;
        analyzer.AnalysisProgressedEvent -= IncreaseProgress;
        Progress = 0f;
        
        AnalysisCompletedEvent.Invoke(result, dates);
    }
}