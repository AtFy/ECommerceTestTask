namespace Lib.DbController.Interfaces;

public interface IDbController
{
    public Task<string> GetTotalGrossForPeriodAsync((DateTime dateStart, DateTime dateFinish) dates);

    public Task<string> GetMostPopularBrandAsync((DateTime dateStart, DateTime dateFinish) dates);

    public Task<string> GetMostPopularCategoryAsync((DateTime dateStart, DateTime dateFinish) dates);

    public Task<string> GetMostPopularProductAsync((DateTime dateStart, DateTime dateFinish) dates);
}