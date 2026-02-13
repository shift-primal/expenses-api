using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<ExpensesDb>(opt => opt.UseSqlite(connectionString));

var app = builder.Build();

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

app.Run();
