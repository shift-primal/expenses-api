using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<ExpensesDb>(opt => opt.UseSqlite(connectionString));

var app = builder.Build();

CategoryEndpoints.Map(app);
CategoryRuleEndpoints.Map(app);
TransactionEndpoints.Map(app);

app.Run();
