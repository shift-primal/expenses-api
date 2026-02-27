using Microsoft.EntityFrameworkCore;

public static class TransactionEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet(
            "/transactions",
            async (
                int pageNumber,
                DateOnly? from,
                DateOnly? to,
                int? categoryId,
                string? merchant,
                string? search,
                string? sortBy,
                string? sortDir,
                ExpensesDb db
            ) =>
            {
                var query = db.Transactions.AsQueryable();

                if (from is not null)
                    query = query.Where(t => t.Date >= from);

                if (to is not null)
                    query = query.Where(t => t.Date <= to);

                if (categoryId is not null)
                    query = query.Where(t => t.CategoryId == categoryId);

                if (merchant is not null)
                    query = query.Where(t => t.Merchant.Contains(merchant));

                if (search is not null)
                    query = query.Where(t => t.Description.Contains(search));

                query = sortBy switch
                {
                    "amount" => sortDir == "desc"
                        ? query.OrderByDescending(t => t.Amount)
                        : query.OrderBy(t => t.Amount),
                    "description" => sortDir == "desc"
                        ? query.OrderByDescending(t => t.Description)
                        : query.OrderBy(t => t.Description),
                    _ => sortDir == "desc"
                        ? query.OrderByDescending(t => t.Date)
                        : query.OrderBy(t => t.Date),
                };

                var result = await query
                    .Include(t => t.Category)
                    .Skip(pageNumber * 25)
                    .Take(25)
                    .ToListAsync();

                return Results.Ok(result);
            }
        );

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

                t.Amount = inputT.Amount;
                t.CategoryId = inputT.CategoryId;
                t.CreatedAt = inputT.CreatedAt;
                t.Date = inputT.Date;
                t.Description = inputT.Description;
                t.Merchant = inputT.Merchant;

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
