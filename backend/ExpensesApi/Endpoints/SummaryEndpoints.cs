using Microsoft.EntityFrameworkCore;

public static class SummaryEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet(
            "/summary/totals",
            async (ExpensesDb db) =>
            {
                var income = await db.Transactions.Where(t => t.Amount > 0).SumAsync(t => t.Amount);
                var expenses = await db
                    .Transactions.Where(t => t.Amount < 0)
                    .SumAsync(t => t.Amount);

                var balance = income + expenses;
                var saved = Decimal.ToInt32((balance / income) * 100);

                return Results.Ok(
                    new
                    {
                        income,
                        expenses,
                        balance,
                        saved,
                    }
                );
            }
        );

        app.MapGet(
            "/summary/by-category",
            async (ExpensesDb db) =>
            {
                var result = await db
                    .Transactions.GroupBy(t => new { t.CategoryId, t.Category!.Name })
                    .Select(g => new
                    {
                        category = g.Key,
                        total = g.Sum(t => t.Amount),
                        count = g.Count(),
                    })
                    .ToListAsync();

                return Results.Ok(result);
            }
        );

        app.MapGet(
            "/summary/by-month",
            async (ExpensesDb db) =>
            {
                var result = await db
                    .Transactions.GroupBy(t => new { t.Date.Year, t.Date.Month })
                    .Select(g => new
                    {
                        year = g.Key.Year,
                        month = g.Key.Month,
                        total = g.Sum(t => t.Amount),
                        count = g.Count(),
                    })
                    .OrderBy(g => g.year)
                    .ThenBy(g => g.month)
                    .ToListAsync();

                var formatted = result.Select(g => new
                {
                    date = $"{g.year}-{g.month:D2}",
                    g.total,
                    g.count,
                });

                return Results.Ok(formatted);
            }
        );
    }
}
