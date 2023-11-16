namespace Lib.Analyzer.Interfaces;

public interface IAnalyzer
{
    public string RunAnalysis((DateOnly dateStart, DateOnly dateFinish) dates);
}