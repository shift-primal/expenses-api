using Microsoft.EntityFrameworkCore;

public static class CategoryEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/categories", async (ExpensesDb db) => await db.Categories.ToListAsync());

        app.MapGet(
            "/categories/{id}",
            async (int id, ExpensesDb db) =>
                await db.Categories.FindAsync(id) is Category c ? Results.Ok(c) : Results.NotFound()
        );

        app.MapPost(
            "/categories",
            async (Category c, ExpensesDb db) =>
            {
                db.Categories.Add(c);
                await db.SaveChangesAsync();

                return Results.Created($"/categories/{c.Id}", c);
            }
        );

        app.MapPut(
            "/categories/{id}",
            async (int id, Category inputC, ExpensesDb db) =>
            {
                var c = await db.Categories.FindAsync(id);

                if (c is null)
                    return Results.NotFound();

                c.Name = inputC.Name;
                c.IsDefault = inputC.IsDefault;
                c.Rules = inputC.Rules;

                await db.SaveChangesAsync();

                return Results.NoContent();
            }
        );

        app.MapDelete(
            "/categories/{id}",
            async (int id, ExpensesDb db) =>
            {
                // TODO
                // Gjøre så transaksjoner med kategorien flyttes til "annet" først

                if (await db.Categories.FindAsync(id) is Category c)
                {
                    db.Categories.Remove(c);
                    await db.SaveChangesAsync();
                    return Results.NoContent();
                }

                return Results.NotFound();
            }
        );
    }
}
