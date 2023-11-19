using System.Text;
using Lib.Analyzer.Interfaces;
using Lib.DbController.Context;
using Lib.DbController.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib.Analyzer;

public class SqlAnalyzer : IAnalyzer
{
    public SqlAnalyzer(IDbController dbController)
    {
        _dbController = dbController;
    }
    private IDbController _dbController;
    public event AnalysisStepProgressedEventHandler AnalysisProgressedEvent;

    private List<Task<string>> _tasks = new();
    public async Task<string> RunAnalysisAsync((DateTime dateStart, DateTime dateFinish) dates)
    {
        var result = new StringBuilder();
        
        _tasks.Add(_dbController.GetTotalGrossForPeriodAsync(dates)
            .ContinueWith((task) => Inv(task.Result).Result));
        _tasks.Add(_dbController.GetMostPopularBrandAsync(dates)
            .ContinueWith((task) => Inv(task.Result).Result));
        _tasks.Add(_dbController.GetMostPopularCategoryAsync(dates)
            .ContinueWith((task) => Inv(task.Result).Result));
        _tasks.Add(_dbController.GetMostPopularProductAsync(dates)
            .ContinueWith((task) => Inv(task.Result).Result));
        
        AnalysisProgressedEvent.Invoke(0.04f);
        
        foreach (var task in _tasks)
        {
            result.Append(task.Result);
        }

        _tasks = new();
        GC.Collect();
        
        return result.ToString();
    }

    private async Task<string> Inv(string res)
    {
        AnalysisProgressedEvent.Invoke(0.24f);
        return res;
    }
}