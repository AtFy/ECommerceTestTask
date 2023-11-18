using Lib.DbController.Context;
using Microsoft.EntityFrameworkCore;

namespace Lib.DbController;

public class DbController
{
    public async Task<string> GetTotalGrossForPeriodAsync((DateTime dateStart, DateTime dateFinish) dates)
    {
        await using var db = new ECommerceContext();
        return $"Total gross for the period is $" +
               $"{await Task.Run(() => GetTotalGrossForPeriod(dates, db))}\n";
    }

    public async Task<string> GetMostPopularBrandAsync((DateTime dateStart, DateTime dateFinish) dates)
    {
        await using var db = new ECommerceContext();
        return $"The most popular brand during the period is <" +
               $"{await Task.Run(() => GetMostPopularBrand(dates, db))}>\n";
    }

    private string GetTotalGrossForPeriod((DateTime dateStart, DateTime dateFinish) dates, ECommerceContext db) =>
        (db as ECommerceContext).Events
            .Where(ev => ev.EventType != null &&
                         ev.EventType.Contains("purchase") &&
                         ev.EventTime >= dates.dateStart &&
                         ev.EventTime <= dates.dateFinish)
            .AsNoTracking()
            .Select(ev => ev.Price)
            .Sum()
            .ToString() ?? "0";

    private string GetMostPopularBrand((DateTime dateStart, DateTime dateFinish) dates, ECommerceContext db) =>
        (db as ECommerceContext).Events
        .Where(ev => ev.Brand != null &&
                     ev.EventTime >= dates.dateStart &&
                     ev.EventTime <= dates.dateFinish)
        .GroupBy(ev => ev.Brand)
        .OrderByDescending(ev => ev.Count())
        .Select(g => new { brand = g.Key, count = g.Count() })
        .Take(1)
        .FirstOrDefault()?
        .brand ?? "undefined";
}