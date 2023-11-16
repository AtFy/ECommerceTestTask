using Lib.Analyzer.Interfaces;

namespace Application.Interfaces;

public interface ILogicWorker
{
    public Task RunAnalysisAsync(IAnalyzer analyzer);

    public bool IsRunning { get; }

    public float Progress { get; }
}