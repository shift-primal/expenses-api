using Microsoft.EntityFrameworkCore;

public static class CategoryRuleEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet(
            "/categories/{id}/rules",
            async (int id, ExpensesDb db) =>
            {
                if (!await db.Categories.AnyAsync(c => c.Id == id))
                    return Results.NotFound();

                var rules = await db.CategoryRules.Where(r => r.CategoryId == id).ToListAsync();
                return Results.Ok(rules);
            }
        );

        app.MapPost(
            "/categories/{id}/rules",
            async (int id, CategoryRule cr, ExpensesDb db) =>
            {
                if (!await db.Categories.AnyAsync(c => c.Id == id))
                    return Results.NotFound();

                cr.CategoryId = id;
                db.CategoryRules.Add(cr);
                await db.SaveChangesAsync();

                return Results.Created($"/categories/{id}/rules/{cr.Id}", cr);
            }
        );

        app.MapDelete(
            "/categories/rules/{id}",
            async (int id, ExpensesDb db) =>
            {
                if (await db.CategoryRules.FindAsync(id) is CategoryRule cr)
                {
                    db.CategoryRules.Remove(cr);
                    await db.SaveChangesAsync();
                    return Results.NoContent();
                }

                return Results.NotFound();
            }
        );
    }
}
