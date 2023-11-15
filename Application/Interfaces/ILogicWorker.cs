namespace Application.Interfaces;

public interface ILogicWorker
{
    public Task RunSqlAnalysisAsync();

    public Task RunNoSqlAnalysisAsync();

    public bool IsRunning { get; }

    public float Progress { get; }
}