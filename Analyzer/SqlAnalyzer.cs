using System.Text;
using Lib.Analyzer.Interfaces;
using Lib.DbController.Context;
using Microsoft.EntityFrameworkCore;

namespace Lib.Analyzer;

public class SqlAnalyzer : IAnalyzer
{
    public event AnalysisStepProgressedEventHandler AnalysisProgressedEvent;

    private List<Task<string>> _tasks = new();
    public async Task<string> RunAnalysisAsync((DateTime dateStart, DateTime dateFinish) dates)
    {
        var dbController = new DbController.DbController();
        
        var result = new StringBuilder();
        result.Clear();
        
        _tasks.Add(dbController.GetTotalGrossForPeriodAsync(dates)
            .ContinueWith((task) => Inv(task.Result).Result));
        _tasks.Add(dbController.GetMostPopularBrandAsync(dates)
            .ContinueWith((task) => Inv(task.Result).Result));
        _tasks.Add(dbController.GetMostPopularCategoryAsync(dates)
            .ContinueWith((task) => Inv(task.Result).Result));
        _tasks.Add(dbController.GetMostPopularProductAsync(dates)
            .ContinueWith((task) => Inv(task.Result).Result));
        
        foreach (var task in _tasks)
        {
            result.Append(task.Result);
        }
        
        GC.Collect();
        return result.ToString();
    }

    private async Task<string> Inv(string res)
    {
        AnalysisProgressedEvent.Invoke();
        return res;
    }
}