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

                return Results.Ok(
                    new
                    {
                        income,
                        expenses,
                        balance = income + expenses,
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
                    .Transactions.GroupBy(t => t.Date.Month)
                    .Select(g => new
                    {
                        month = g.Key,
                        total = g.Sum(t => t.Amount),
                        count = g.Count(),
                    })
                    .ToListAsync();

                return Results.Ok(result);
            }
        );
    }
}
