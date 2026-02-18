using System.Globalization;
using Microsoft.EntityFrameworkCore;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("nb-NO");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("nb-NO");

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<ExpensesDb>(opt => opt.UseSqlite(connectionString));

builder.Services.AddOpenApi();

var app = builder.Build();

CategoryEndpoints.Map(app);
CategoryRuleEndpoints.Map(app);
TransactionEndpoints.Map(app);

app.MapOpenApi();

var parser = new CsvParser();
var stream = File.OpenRead("../../../../Personlig/data/kasper/transactions.txt");
var rows = parser.ParseRows(stream);

foreach (var r in rows)
    Console.WriteLine(r);

app.Run();
