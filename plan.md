# Full Stack Personal Expenses Tracker — Project Plan

## Project Overview

A full-stack expenses tracker that imports Norwegian bank CSV statements, stores them in a .NET backend, and visualizes spending through a React dashboard. Built as a learning project covering Python data processing, .NET Minimal APIs, and React frontend development.

**Tech Stack:**

- **Data Pipeline:** Python + Pandas (CSV parsing, cleaning, auto-categorization)
- **Backend:** .NET 8 Minimal API + Entity Framework Core + SQLite
- **Frontend:** React (Vite) + a charting library (Recharts or Chart.js)
- **Future:** Docker, Azure deployment

---

## Chapter 1: Project Setup & Architecture

### 1.1 — Repository Structure

```
expenses-tracker/
├── python/                  # CSV parser & categorizer
│   ├── parser.py
│   ├── categorizer.py
│   ├── requirements.txt
│   └── sample_data/         # Anonymized test CSVs
├── backend/                 # .NET Minimal API
│   ├── ExpensesApi/
│   │   ├── Program.cs
│   │   ├── Models/
│   │   ├── Data/
│   │   ├── Endpoints/
│   │   └── expenses.db      # SQLite (gitignored)
│   └── ExpensesApi.sln
├── frontend/                # React app
│   ├── src/
│   ├── package.json
│   └── vite.config.ts
├── docker-compose.yml       # (Chapter 7)
├── .gitignore
└── README.md
```

### 1.2 — Data Model (Three Tables)

Three tables — enough structure to be useful, not so much that you're fighting your own schema.

**Transactions** — the core table, one row per bank transaction:

```
Transactions
├── Id                  (int, PK, auto-increment)
├── Date                (DateOnly)
├── Description         (string)          ← raw text from CSV
├── Amount              (decimal)          ← negative = expense, positive = income
├── CategoryId          (int, FK → Categories)
├── AccountSource       (string?)          ← "DNB Brukskonto" etc.
├── ImportBatchId       (Guid)             ← groups one CSV upload
├── RawLine             (string?)          ← original CSV line for debugging
├── CreatedAt           (DateTime)
```

> **Why `ImportBatchId`?** It lets you undo an entire import if something went wrong, and prevents duplicate imports by checking if the same batch already exists.

**Categories** — a separate table rather than a plain string on the transaction:

```
Categories
├── Id                  (int, PK)
├── Name                (string)           ← "Groceries", "Transport"
├── IsDefault           (bool)             ← seed categories vs user-created
```

> **Why a separate table?** Rename a category in one place instead of updating thousands of rows. Later you can add icons, colors, or monthly budget targets per category.

**CategoryRules** — the keyword mapping table for auto-categorization:

```
CategoryRules
├── Id                  (int, PK)
├── Keyword             (string)           ← "kiwi", "amzn mktp", "ruter"
├── CategoryId          (int, FK → Categories)
├── Source              (string)           ← "seed", "api", "user"
├── CreatedAt           (DateTime)
```

**C# Models:**

```csharp
public class Transaction
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }      // navigation property
    public string? AccountSource { get; set; }
    public Guid ImportBatchId { get; set; }
    public string? RawLine { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsDefault { get; set; }
    public List<Transaction> Transactions { get; set; }
    public List<CategoryRule> Rules { get; set; }
}

public class CategoryRule
{
    public int Id { get; set; }
    public string Keyword { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public string Source { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

EF Core will create the foreign keys and relationships from the navigation properties.

### 1.3 — Suggested Change: Skip Python, Do It All in .NET

Your original flow has Python parsing CSVs and then somehow getting data into .NET. This creates an awkward handoff — how does the parsed data reach the API? You'd need to either:

- Write parsed data to a JSON/CSV file and then import it via API (clunky)
- Call the API from Python via HTTP (extra complexity)

**My recommendation:** Keep Python for an **initial exploration/prototyping phase** (Chapter 2), where you analyze the CSV format, experiment with Pandas, and build the categorization keyword map. Then implement the actual CSV parsing and import **inside the .NET API** as an upload endpoint. This way the full pipeline lives in one place.

**However**, since this is a learning project and you want Python/Pandas experience, here's how we'll do both:

| Phase               | Tool              | Purpose                                  |
| ------------------- | ----------------- | ---------------------------------------- |
| Explore & prototype | Python + Pandas   | Understand the CSV, build category rules |
| Production pipeline | .NET API endpoint | Upload CSV → parse → categorize → store  |

---

## Chapter 2: CSV Exploration with Python (Learning Phase)

**Goal:** Understand Norwegian bank CSV formats and build categorization rules.

### 2.1 — Gather Sample Data

Download CSV exports from your bank(s). Norwegian banks typically export in formats like:

**DNB format (semicolon-separated, ISO-8859-1 encoded):**

```
"Dato";"Forklaring";"Rentedato";"Ut fra konto";"Inn på konto"
"17.01.2025";"REMA 1000 STORO";"17.01.2025";"249,00";""
"16.01.2025";"Spotify AB";"16.01.2025";"119,00";""
"15.01.2025";"LØNN ARBEIDSGIVER AS";"15.01.2025";"";"35000,00"
```

Key quirks to handle: semicolon delimiter, Norwegian date format (dd.mm.yyyy), comma as decimal separator, separate columns for debit/credit, ISO-8859-1 or UTF-8 encoding.

### 2.2 — Parse & Clean with Pandas

```python
import pandas as pd

df = pd.read_csv(
    "dnb_export.csv",
    sep=";",
    encoding="iso-8859-1",  # or utf-8, check your bank
    decimal=",",
    dayfirst=True,
    parse_dates=["Dato"],
    quotechar='"'
)

# Normalize: merge debit/credit into single Amount column
df["Amount"] = df["Inn på konto"].fillna(0) - df["Ut fra konto"].fillna(0)
```

### 2.3 — Build the Category Keyword Map

This is the most valuable output from the Python phase — a JSON mapping of keywords to categories:

```json
{
  "Groceries": ["rema", "kiwi", "meny", "coop", "bunnpris", "joker", "spar"],
  "Transport": ["ruter", "vy", "oslo taxi", "circle k", "esso", "shell"],
  "Dining": ["restaurant", "café", "pizza", "sushi", "burger", "starbucks"],
  "Subscriptions": ["spotify", "netflix", "hbo", "viaplay", "youtube"],
  "Rent": ["husleie", "bolig"],
  "Salary": ["lønn"],
  "Shopping": ["h&m", "zara", "ikea", "elkjøp", "power", "komplett"],
  "Health": ["apotek", "vitus", "tannlege", "lege"],
  "Entertainment": ["kino", "ticketmaster", "billettservice"]
}
```

Write a simple categorizer function, test it on your data, and iterate until you're happy with the coverage. This JSON file will be reused in the .NET backend.

### 2.4 — Deliverables from This Chapter

- [ ] A working Jupyter notebook or Python script that parses your bank's CSV
- [ ] A `categories.json` keyword map with decent coverage
- [ ] Understanding of your bank's data quirks (encoding, delimiters, date formats)

---

## Chapter 3: .NET Backend — Data Layer

**Goal:** Set up the .NET project with SQLite, Entity Framework Core, and the data model.

### 3.1 — Create the Project

```bash
dotnet new web -n ExpensesApi
cd ExpensesApi
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
```

### 3.2 — Define the Models

Create the three model classes from Chapter 1.2:

- `Models/Transaction.cs`
- `Models/Category.cs`
- `Models/CategoryRule.cs`

### 3.3 — Set Up DbContext

Create `Data/ExpensesDbContext.cs` with `DbSet<Transaction>`, `DbSet<Category>`, and `DbSet<CategoryRule>`. Configure the relationships and any indexes (e.g. index on `CategoryRule.Keyword` for fast lookups).

### 3.4 — Create & Run Migrations

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 3.5 — Seed Categories and Keyword Rules

Use EF Core's `HasData()` or a startup seeder to populate the Categories table with defaults (Groceries, Transport, Dining, Subscriptions, Rent, Salary, Shopping, Health, Entertainment, Other) and the CategoryRules table with the keyword mappings from your Python exploration (Chapter 2). Mark these with `Source = "seed"`.

### Deliverables

- [ ] .NET project with EF Core + SQLite configured
- [ ] Three models with proper relationships and initial migration
- [ ] Seeded categories and keyword rules
- [ ] A working `expenses.db` file

---

## Chapter 4: .NET Backend — API Endpoints & Categorization

**Goal:** Build the Minimal API endpoints and the three-tier auto-categorization system.

### 4.1 — Full Endpoint Plan

**Transactions — main CRUD:**

| Method   | Route                    | Purpose                                                               |
| -------- | ------------------------ | --------------------------------------------------------------------- |
| `GET`    | `/api/transactions`      | List with filters (date range, category, search, pagination, sorting) |
| `GET`    | `/api/transactions/{id}` | Single transaction detail                                             |
| `PUT`    | `/api/transactions/{id}` | Update — mainly recategorizing, but could edit any field              |
| `DELETE` | `/api/transactions/{id}` | Delete a single transaction                                           |

**Import — CSV upload:**

| Method   | Route                                 | Purpose                                              |
| -------- | ------------------------------------- | ---------------------------------------------------- |
| `POST`   | `/api/import`                         | Upload CSV file → parse → categorize → store         |
| `GET`    | `/api/import/batches`                 | List all imports (date, filename, transaction count) |
| `DELETE` | `/api/import/batches/{importBatchId}` | Undo an entire import                                |

**Categories:**

| Method   | Route                  | Purpose                                                    |
| -------- | ---------------------- | ---------------------------------------------------------- |
| `GET`    | `/api/categories`      | List all categories (for dropdowns, filters, chart labels) |
| `POST`   | `/api/categories`      | Create a new category                                      |
| `PUT`    | `/api/categories/{id}` | Rename a category                                          |
| `DELETE` | `/api/categories/{id}` | Delete (reassign transactions to "Other" first)            |

**Category Rules — keyword mappings:**

| Method   | Route                            | Purpose                      |
| -------- | -------------------------------- | ---------------------------- |
| `GET`    | `/api/categories/{id}/rules`     | List keywords for a category |
| `POST`   | `/api/categories/{id}/rules`     | Add a keyword rule           |
| `DELETE` | `/api/categories/rules/{ruleId}` | Remove a rule                |

**Summary — dashboard aggregations (all accept `?from=...&to=...`):**

| Method | Route                      | Purpose                                      |
| ------ | -------------------------- | -------------------------------------------- |
| `GET`  | `/api/summary/totals`      | Total income, expenses, balance              |
| `GET`  | `/api/summary/by-category` | Spending per category (for pie chart)        |
| `GET`  | `/api/summary/by-month`    | Monthly income vs expenses (for trend chart) |

### 4.2 — Three-Tier Auto-Categorization System

This is the engine that makes the import smart. Each transaction flows through three tiers:

```
Transaction comes in
    → Tier 1: Check CategoryRules table         (instant, free)
        Match? → assign category
        No match? ↓
    → Tier 2: Call Anthropic API                 (fast, near-free)
        Confident? → assign + save keyword to CategoryRules (Source = "api")
        Unsure? ↓
    → Tier 3: Mark as "Other"                    (user fixes in UI)
        User recategorizes → save keyword to CategoryRules (Source = "user")
```

**Tier 1 — Keyword dictionary lookup:** Case-insensitive _contains_ matching against the CategoryRules table. Bank descriptions are messy (e.g. `"KIWI 487 GRÜNERLØKKA 17.01"`), so check if the description contains any known keyword. Order checks from most specific to least specific to avoid false matches.

**Tier 2 — Anthropic API fallback:** Send the transaction description and ask for a category from your existing category list. Keep the prompt tight:

> "Given these categories: [Groceries, Transport, Dining, ...], categorize this transaction: 'AMZN MKTP SE 4R2X1'. Respond with only the category name."

Cost is negligible for personal use — you only hit this for the ~20-30% of transactions the dictionary misses, and each call is tiny.

**Tier 3 — "Other" with manual override:** Transactions the API can't confidently categorize get marked as "Other". The frontend lets users click to recategorize.

**The learning loop (key feature):** When Tier 2 or Tier 3 assigns a category, save the keyword mapping back into the CategoryRules table. Next time the same merchant appears, Tier 1 catches it. The system gets smarter over time without manual maintenance.

### 4.3 — CSV Import Endpoint (The Core Feature)

`POST /api/import` should:

1. Accept a CSV file via `IFormFile`
2. Detect the bank format (DNB, Nordea, etc.) based on header row
3. Parse rows using the correct column mapping
4. Run each transaction through the three-tier categorizer
5. Assign an `ImportBatchId` to the batch
6. Check for duplicates (same date + amount + description)
7. Save to database
8. Return a summary:

```json
{
  "importBatchId": "a1b2c3...",
  "totalRows": 142,
  "imported": 138,
  "skippedDuplicates": 4,
  "byCategory": [
    { "category": "Groceries", "count": 34 },
    { "category": "Other", "count": 12 }
  ]
}
```

### 4.4 — Query & Filter Endpoint

`GET /api/transactions` should support query parameters:

```
?from=2025-01-01&to=2025-01-31    // date range
&categoryId=3                       // filter by category
&search=rema                        // text search in description
&minAmount=-500&maxAmount=0         // amount range
&page=1&pageSize=50                 // pagination
&sortBy=date&sortDir=desc           // sorting
```

### 4.5 — Summary Endpoints for Dashboard

Backend does the heavy aggregation — the frontend just feeds the numbers into charts. Example response from `GET /api/summary/by-category?from=2025-01-01&to=2025-03-31`:

```json
[
  {
    "categoryId": 1,
    "categoryName": "Groceries",
    "total": -4200.0,
    "count": 18
  },
  {
    "categoryId": 4,
    "categoryName": "Subscriptions",
    "total": -587.0,
    "count": 4
  }
]
```

### 4.6 — Organize with Endpoint Groups

Keep it clean by putting related endpoints in separate static classes:

```
Endpoints/
├── TransactionEndpoints.cs
├── ImportEndpoints.cs
├── CategoryEndpoints.cs
├── CategoryRuleEndpoints.cs
└── SummaryEndpoints.cs
```

Map them in `Program.cs`:

```csharp
app.MapTransactionEndpoints();
app.MapImportEndpoints();
app.MapCategoryEndpoints();
app.MapCategoryRuleEndpoints();
app.MapSummaryEndpoints();
```

### 4.7 — Enable CORS

Your React frontend will run on a different port during development:

```csharp
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()));
```

### 4.8 — Suggested Build Order

Implement in this order so you always have data to work with:

1. Categories (GET, POST) — needed before transactions make sense
2. Import (POST) — so you have actual data
3. Transactions (GET with filters) — query your data
4. Summary endpoints — feed the dashboard
5. Everything else (PUT, DELETE, rules, batches)

### Deliverables

- [ ] All endpoints working and testable via Swagger/Bruno/Postman
- [ ] Three-tier categorization working (dictionary → API → Other)
- [ ] Learning loop saving new keyword rules from Tier 2 and Tier 3
- [ ] CSV import successfully parses, categorizes, and stores transactions
- [ ] Duplicate detection works
- [ ] Filtering, pagination, and summary aggregation work

---

## Chapter 5: React Frontend — Setup & Data Layer

**Goal:** Set up the React project, connect to the API, and build the data layer.

### 5.1 — Create the Project

```bash
npm create vite@latest frontend -- --template react-ts
cd frontend
npm install
npm install axios recharts react-router-dom
npm install -D tailwindcss @tailwindcss/vite
```

### 5.2 — Project Structure

```
src/
├── api/
│   └── transactionApi.ts     # Axios instance + API calls
├── components/
│   ├── layout/
│   │   ├── Sidebar.tsx
│   │   └── Layout.tsx
│   ├── dashboard/
│   │   ├── SpendingByCategory.tsx   # Pie/donut chart
│   │   ├── MonthlyTrend.tsx         # Line/bar chart
│   │   ├── SummaryCards.tsx          # Total income, expenses, balance
│   │   └── RecentTransactions.tsx
│   ├── transactions/
│   │   ├── TransactionTable.tsx
│   │   ├── TransactionFilters.tsx
│   │   └── CategoryBadge.tsx
│   └── import/
│       ├── CsvUpload.tsx             # Drag & drop file upload
│       └── ImportSummary.tsx
├── pages/
│   ├── DashboardPage.tsx
│   ├── TransactionsPage.tsx
│   └── ImportPage.tsx
├── types/
│   └── transaction.ts
├── hooks/
│   └── useTransactions.ts
├── App.tsx
└── main.tsx
```

### 5.3 — API Service Layer

Create a typed API client that mirrors your backend endpoints:

```typescript
// api/transactionApi.ts
const api = axios.create({ baseURL: "http://localhost:5000/api" });

export const getTransactions = (filters: TransactionFilters) => ...
export const getSummary = () => ...
export const uploadCsv = (file: File) => ...
export const updateTransaction = (id: number, data: Partial<Transaction>) => ...
```

### Deliverables

- [ ] React project scaffolded with routing
- [ ] API client connecting to backend
- [ ] TypeScript types matching the backend model

---

## Chapter 6: React Frontend — Dashboard & Features

**Goal:** Build the actual UI — dashboard, transaction list, and CSV import.

### 6.1 — Dashboard Page

The main landing page with four key widgets:

1. **Summary Cards** — Total income, total expenses, net balance (current month + all time)
2. **Spending by Category** — Pie or donut chart (Recharts `PieChart`)
3. **Monthly Trend** — Bar chart showing expenses vs income over time (`BarChart`)
4. **Recent Transactions** — Last 5-10 transactions as a compact list

### 6.2 — Transactions Page

A full table view with:

- **Filters:** Date range picker, category dropdown, text search
- **Sortable columns:** Date, description, amount, category
- **Inline category editing:** Click a category badge to change it (calls `PUT` endpoint)
- **Pagination:** Load more or page numbers

### 6.3 — Import Page

- **Drag & drop zone** for CSV files (or click to browse)
- **Preview** parsed data before confirming import
- **Import summary** showing what was added, skipped, and categorized
- **Bank selector** if you support multiple formats

### 6.4 — Nice-to-Have Features (Add Later)

- Date range selector on dashboard (this month / last 3 months / year / custom)
- Category management page (edit keyword mappings)
- Dark mode
- Export filtered data as CSV
- Monthly budget targets with progress bars

### Deliverables

- [ ] Dashboard with working charts pulling real data from the API
- [ ] Transaction list with filtering, sorting, and inline category editing
- [ ] CSV upload flow with preview and confirmation
- [ ] Responsive layout that works on different screen sizes

---

## Chapter 7: Dockerize & Prepare for Deployment

**Goal:** Containerize all parts for easy deployment.

### 7.1 — Dockerfiles

Create a `Dockerfile` for the .NET backend and one for the React frontend (served via Nginx).

### 7.2 — Docker Compose

```yaml
version: "3.8"
services:
  backend:
    build: ./backend
    ports:
      - "5000:8080"
    volumes:
      - db-data:/app/data # Persist SQLite

  frontend:
    build: ./frontend
    ports:
      - "3000:80"
    depends_on:
      - backend

volumes:
  db-data:
```

### 7.3 — Azure Deployment (When Ready)

Options to consider:

- **Azure Container Apps** — simplest for Docker containers
- **Azure App Service** — good for .NET specifically
- **Azure Static Web Apps** — free tier for the React frontend

For a learning project, Azure Container Apps with a free tier is the easiest path.

### Deliverables

- [ ] Both services running via `docker-compose up`
- [ ] SQLite data persisted via Docker volume
- [ ] (Optional) Deployed to Azure

---

## Chapter 8: Polish & Extensions

Once the core is working, consider these improvements:

### Testing

- **Backend:** xUnit integration tests for the import endpoint
- **Frontend:** React Testing Library for key components

### Data Quality

- Smarter duplicate detection (fuzzy matching on description)
- Handle bank-specific edge cases (refunds, transfers between own accounts)
- Let users define custom category rules from the UI

### Potential Upgrades

- Swap SQLite for PostgreSQL if you want to learn relational DB ops
- Add authentication (ASP.NET Identity or Auth0) if deploying publicly
- Build a simple budget feature: set monthly limits per category, show progress

---

## Suggested Build Order (Timeline)

| Week | Chapter | Focus                                                    |
| ---- | ------- | -------------------------------------------------------- |
| 1    | Ch 1-2  | Setup repo, explore CSVs with Python, build category map |
| 2    | Ch 3    | .NET project, EF Core, SQLite, data model                |
| 3    | Ch 4    | API endpoints, CSV import in .NET, Swagger testing       |
| 4    | Ch 5    | React scaffold, API client, routing                      |
| 5    | Ch 6    | Dashboard charts, transaction table, CSV upload UI       |
| 6    | Ch 7    | Docker, deployment                                       |
| 7+   | Ch 8    | Polish, testing, extensions                              |

This is flexible — some chapters might take a few days, others a full week depending on your schedule. The important thing is to get a **working vertical slice** (import → store → display) as early as possible, then iterate.

---

_Good luck — this is a really solid project for learning full-stack development. Let me know when you're ready to start on any chapter and I'll help you build it!_
