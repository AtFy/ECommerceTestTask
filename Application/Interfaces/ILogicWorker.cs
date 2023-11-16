using Lib.Analyzer.Interfaces;

namespace Application.Interfaces;

public interface ILogicWorker
{
    public Task RunAnalysisAsync(IAnalyzer analyzer, (DateOnly dateStart, DateOnly dateFinish) dates);

    public bool IsRunning { get; }

    public float Progress { get; }
}