using System.Globalization;
using Microsoft.EntityFrameworkCore;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("nb-NO");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("nb-NO");

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<ExpensesDb>(opt => opt.UseSqlite(connectionString));

builder.Services.AddScoped<CsvParser>();
builder.Services.AddScoped<Categorizer>();

builder.Services.AddOpenApi();
builder.Services.AddAntiforgery();

var app = builder.Build();

CategoryEndpoints.Map(app);
CategoryRuleEndpoints.Map(app);
TransactionEndpoints.Map(app);

app.MapOpenApi();

var parser = new CsvParser();

var stream = File.OpenRead("../../../../Personlig/data/kasper/transactions.txt");
var reader = new StreamReader(stream);
var rows = parser.ParseRows(reader);

Console.WriteLine(rows[1]);

app.Run();
