using System.Diagnostics;
using Lib.DbController.Interfaces;

namespace Lib.DbController;

public class ResilientDbController : IDbController
{
    public ResilientDbController(IDbController dbController)
    {
        _dbController = dbController;
    }

    private IDbController _dbController;
    private const int FIVE_SECONDS = 5000;

    public async Task<string> GetTotalGrossForPeriodAsync((DateTime dateStart, DateTime dateFinish) dates)
    {
        return await Try(_dbController.GetTotalGrossForPeriodAsync(dates));
    }

    public async Task<string> GetMostPopularBrandAsync((DateTime dateStart, DateTime dateFinish) dates)
    {
        return await Try(_dbController.GetMostPopularBrandAsync(dates));
    }

    public async Task<string> GetMostPopularCategoryAsync((DateTime dateStart, DateTime dateFinish) dates)
    {
        return await Try(_dbController.GetMostPopularCategoryAsync(dates));
    }

    public async Task<string> GetMostPopularProductAsync((DateTime dateStart, DateTime dateFinish) dates)
    {
       return await Try(_dbController.GetMostPopularProductAsync(dates));
    }

    private async Task<string> Try(Task<string> task)
    { 
        var retryCounter = 0;
        while (true)
        {
            try
            {
                return await task;
            }
            catch (Exception)
            {
                if (retryCounter > 4)
                {
                    throw;
                }

                ++retryCounter;
                Thread.Sleep(FIVE_SECONDS);
            }
        }
    }
}