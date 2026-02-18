using Microsoft.EntityFrameworkCore;

public static class ImportEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapPost(
            "/import",
            async (IFormFile csv, ExpensesDb db, CsvParser parser, Categorizer categorizer) =>
            {
                List<Transaction> transactions = [];
                var reader = new StreamReader(csv.OpenReadStream());
                var rows = parser.ParseRows(reader);

                var batchId = Guid.NewGuid();

                foreach (var r in rows)
                {
                    var categoryId = categorizer.Categorize(r.Description);

                    transactions.Add(
                        new Transaction
                        {
                            Date = r.Date,
                            Description = r.Description,
                            Amount = r.Amount,
                            CategoryId = categoryId,
                            ImportBatchId = batchId,
                            RawLine = r.RawLine,
                            CreatedAt = DateTime.UtcNow,
                        }
                    );
                }

                db.Transactions.AddRange(transactions);
                await db.SaveChangesAsync();

                return Results.Ok();
            }
        );

        app.MapGet(
            "/import/batches",
            async (ExpensesDb db) =>
            {
                var result = await db
                    .Transactions.GroupBy(t => t.ImportBatchId)
                    .Select(g => new
                    {
                        importBatchId = g.Key,
                        count = g.Count(),
                        date = g.Min(t => t.CreatedAt),
                    })
                    .ToListAsync();

                return Results.Ok(result);
            }
        );

        app.MapDelete(
            "/import/batches/{id}",
            async (Guid id, ExpensesDb db) =>
            {
                var g = await db.Transactions.Where(t => t.ImportBatchId == id).ToListAsync();

                if (g.Count == 0)
                    return Results.NotFound();

                db.Transactions.RemoveRange(g);
                await db.SaveChangesAsync();

                return Results.NoContent();
            }
        );
    }
}
