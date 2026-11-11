# expenses-api

Personal expenses tracker for Norwegian bank exports. Upload a CSV from DNB or Valle, and the app parses it, sorts each transaction into a category, and shows where the money goes.

This was the first version of what became [Wastescope](https://github.com/shift-primal/wastescope), built to learn ASP.NET Core minimal APIs and EF Core.

## Features

- CSV import for DNB and Valle exports, detected from the header row (CsvHelper)
- Merchant name extraction from raw bank descriptions
- Rule-based categorization: each category has text rules, editable from the UI
- Import batches that can be listed and undone
- Summaries by category and by month, with charts and a transaction table in the React frontend

## Stack

- **Backend:** .NET 10 minimal API, EF Core with SQLite, OpenAPI, xUnit tests for the CSV parser
- **Frontend:** React, Vite, TanStack Query and Table, Recharts, shadcn. API types are generated from the OpenAPI spec.
- **python/:** early prototype of the parser and an LLM-based categorizer

## Run

Backend (.NET 10 SDK):

```sh
cd backend/ExpensesApi
dotnet tool install --global dotnet-ef   # once
dotnet ef migrations add Init            # migrations aren't committed
dotnet ef database update
dotnet run                               # http://localhost:5071
```

Frontend:

```sh
cd frontend/expenses-tracker
pnpm install
pnpm dev     # http://localhost:5173
pnpm types   # regenerate API types while the backend is running
```

`sample-data/dnb-sample.csv` is a made-up DNB export you can import to try it out.

## API

| Route | |
| --- | --- |
| `POST /import` | Upload a CSV |
| `GET /import/batches`, `DELETE /import/batches/{id}` | List and undo imports |
| `GET/PUT/DELETE /transactions/{id}` | Transactions |
| `/categories`, `/categories/{id}/rules` | Categories and their rules |
| `GET /summary/totals`, `/summary/by-category`, `/summary/by-month` | Dashboard data |

Tests: `dotnet test backend/ExpensesApi.Tests`.
