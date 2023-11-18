using Lib.Analyzer.Interfaces;

namespace Application.Interfaces;

public interface ILogicWorker
{
    public Task RunAnalysisAsync(IAnalyzer analyzer, (DateTime dateStart, DateTime dateFinish) dates);

    public bool IsRunning { get; }

    public float Progress { get; }
}