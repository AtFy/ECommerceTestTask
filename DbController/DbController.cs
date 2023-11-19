using Lib.DbController.Context;
using Lib.DbController.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib.DbController;

public class DbController : IDbController
{
    public async Task<string> GetTotalGrossForPeriodAsync((DateTime dateStart, DateTime dateFinish) dates)
    {
        await using var db = new ECommerceContext();
        return $"> Total gross for the period is $" +
               $"{await Task.Run(() => GetTotalGross(dates, db))}\n";
    }

    public async Task<string> GetMostPopularBrandAsync((DateTime dateStart, DateTime dateFinish) dates)
    {
        await using var db = new ECommerceContext();
        return $"> The most popular brand during the period is <" +
               $"{await Task.Run(() => GetMostPopularBrand(dates, db))}>\n";
    }

    public async Task<string> GetMostPopularCategoryAsync((DateTime dateStart, DateTime dateFinish) dates)
    {
        await using var db = new ECommerceContext();
        return $"> The most popular category Id during the period is <" +
               $"{await Task.Run(() => GetMostPopularCategory(dates, db))}>\n";
    }

    public async Task<string> GetMostPopularProductAsync((DateTime dateStart, DateTime dateFinish) dates)
    {
        await using var db = new ECommerceContext();
        return $"> The most popular product Id during the period is <" +
               $"{await Task.Run(() => GetMostPopularProduct(dates, db))}>\n";
    }

    private string GetTotalGross((DateTime dateStart, DateTime dateFinish) dates, ECommerceContext db) =>
        db.Events
            .Where(ev => ev.EventType != null &&
                         string.Compare(ev.EventType, "purchase") == 0 &&
                         ev.EventTime >= dates.dateStart &&
                         ev.EventTime <= dates.dateFinish)
            .AsNoTracking()
            .Select(ev => ev.Price)
            .Sum()
            .ToString() ?? "0";

    // Not only purchases; any actions.
    private string GetMostPopularBrand((DateTime dateStart, DateTime dateFinish) dates, ECommerceContext db) =>
        db.Events
            .Where(ev => ev.Brand != null &&
                         ev.EventTime >= dates.dateStart &&
                         ev.EventTime <= dates.dateFinish)
            .AsNoTracking()
            .GroupBy(ev => ev.Brand)
            .OrderByDescending(ev => ev.Count())
            .Select(group => new { brand = group.Key, count = group.Count() })
            .Take(1)
            .FirstOrDefault()?
            .brand ?? "undefined";

    // Not only purchases; any actions.
    private string GetMostPopularCategory((DateTime dateStart, DateTime dateFinish) dates, ECommerceContext db) =>
        db.Events
            .Where(ev => ev.CategoryId != null &&
                         ev.EventTime >= dates.dateStart &&
                         ev.EventTime <= dates.dateFinish)
            .AsNoTracking()
            .GroupBy(ev => ev.CategoryId)
            .OrderByDescending(ev => ev.Count())
            .Select(group => new { category = group.Key, count = group.Count() })
            .Take(1)
            .FirstOrDefault()?
            .category
            .ToString() ?? "undefined";

    // Not only purchases; any actions.
    private string GetMostPopularProduct((DateTime dateStart, DateTime dateFinish) dates, ECommerceContext db) =>
        db.Events
            .Where(ev => ev.ProductId != null &&
                         ev.EventTime >= dates.dateStart &&
                         ev.EventTime <= dates.dateFinish)
            .AsNoTracking()
            .GroupBy(ev => ev.ProductId)
            .OrderByDescending(ev => ev.Count())
            .Select(group => new { product = group.Key, count = group.Count() })
            .Take(1)
            .FirstOrDefault()?
            .product
            .ToString() ?? "undefined";
}