namespace Lib.Analyzer.Interfaces;

public delegate void AnalysisStepProgressedEventHandler(float stepSize);
public interface IAnalyzer
{
    public Task<string> RunAnalysisAsync((DateTime dateStart, DateTime dateFinish) dates);

    public event AnalysisStepProgressedEventHandler AnalysisProgressedEvent;
}