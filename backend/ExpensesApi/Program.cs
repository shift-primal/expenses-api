using System.Globalization;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("nb-NO");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("nb-NO");

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddCors(opts =>
    opts.AddPolicy(
        name: MyAllowSpecificOrigins,
        policy => policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod()
    )
);

builder.Services.AddDbContext<ExpensesDb>(opt => opt.UseSqlite(connectionString));

builder.Services.ConfigureHttpJsonOptions(opts =>
    opts.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);

builder.Services.AddScoped<CsvParser>();
builder.Services.AddScoped<Categorizer>();

builder.Services.AddOpenApi();

var app = builder.Build();

CategoryEndpoints.Map(app);
CategoryRuleEndpoints.Map(app);
TransactionEndpoints.Map(app);
ImportEndpoints.Map(app);

app.MapOpenApi();
app.UseCors(MyAllowSpecificOrigins);

app.Run();
