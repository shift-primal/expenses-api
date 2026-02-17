using Microsoft.EntityFrameworkCore;

public static class TransactionEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/transactions", async (ExpensesDb db) => await db.Transactions.ToListAsync());

        app.MapGet(
            "/transactions/{id}",
            async (Guid id, ExpensesDb db) =>
                await db.Transactions.FindAsync(id) is Transaction t
                    ? Results.Ok(t)
                    : Results.NotFound()
        );

        app.MapPut(
            "/transactions/{id}",
            async (Guid id, Transaction inputT, ExpensesDb db) =>
            {
                var t = await db.Transactions.FindAsync(id);

                if (t is null)
                    return Results.NotFound();

                t.AccountSource = inputT.AccountSource;
                t.Amount = inputT.Amount;
                t.Category = inputT.Category;
                t.CreatedAt = inputT.CreatedAt;
                t.Date = inputT.Date;
                t.Description = inputT.Description;

                await db.SaveChangesAsync();

                return Results.NoContent();
            }
        );

        app.MapDelete(
            "/transactions/{id}",
            async (Guid id, ExpensesDb db) =>
            {
                if (await db.Transactions.FindAsync(id) is Transaction t)
                {
                    db.Transactions.Remove(t);
                    await db.SaveChangesAsync();
                    return Results.NoContent();
                }

                return Results.NotFound();
            }
        );
    }
}
