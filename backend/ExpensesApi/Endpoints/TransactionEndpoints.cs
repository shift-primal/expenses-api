using Microsoft.EntityFrameworkCore;

public static class TransactionEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/transactions", async (ExpensesDb db) => await db.Transactions.ToListAsync());

        app.MapPost(
            "/transactions",
            async (Transaction t, ExpensesDb db) =>
            {
                db.Transactions.Add(t);
                await db.SaveChangesAsync();

                return Results.Created($"/transactions/{t.Id}", t);
            }
        );
    }
}
