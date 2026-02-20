# Frontend Plan — Expenses Tracker

## Current State

- React + Vite + TypeScript + Tailwind + shadcn/ui
- Routing set up with `react-router`
- Axios client + React Query for data fetching
- Auto-generated types from OpenAPI spec (`auto_types.ts`)
- Two pages: `HomePage` (dashboard, WIP) and `ImportPage` (WIP)
- Hooks: `useTransaction`, `useSummary`, `useImport`

---

## Project Structure (Target)

```
src/
├── api/
│   ├── auto_types.ts          # generated — do not edit
│   └── client.ts              # axios instance
├── components/
│   ├── layout/
│   │   ├── PageLayout.tsx     # done — wraps pages
│   │   ├── Sidebar.tsx        # todo — navigation
│   │   └── Navbar.tsx         # todo — optional top bar
│   ├── dashboard/
│   │   ├── TotalsSummary.tsx          # todo — 3 stat cards
│   │   ├── SpendingByCategoryChart.tsx # todo — pie/donut chart
│   │   ├── SpendingByMonthChart.tsx    # todo — bar chart
│   │   └── RecentTransactions.tsx     # todo — compact table
│   ├── transactions/
│   │   ├── TransactionTable.tsx       # wip — shadcn Table
│   │   ├── TransactionFilters.tsx     # todo — search, date, category
│   │   ├── TransactionRow.tsx         # todo — single row
│   │   └── CategoryBadge.tsx          # todo — inline category pill
│   ├── import/
│   │   ├── CsvUpload.tsx      # wip — file input + validation
│   │   └── ImportSummary.tsx  # wip — results after upload
│   └── shadcn/ui/             # shadcn components — do not edit
├── hooks/
│   ├── useTransaction.ts      # done
│   ├── useSummary.ts          # done
│   └── useImport.ts           # done
├── pages/
│   ├── HomePage.tsx           # wip
│   ├── TransactionsPage.tsx   # todo
│   └── ImportPage.tsx         # wip
├── types.ts                   # manual types + re-exports from auto_types
├── App.tsx                    # routing
└── main.tsx
```

---

## Pages

### HomePage (Dashboard)

The main landing page. Pulls from three summary endpoints and displays them as visual widgets.

```tsx
<PageLayout>
  <TotalsSummary />
  <div className="grid grid-cols-2 gap-4">
    <SpendingByCategoryChart />
    <SpendingByMonthChart />
  </div>
  <RecentTransactions />
</PageLayout>
```

**Data needed:**
- `useSummary<Totals>("totals")` — income, expenses, balance
- `useSummary<CategorySummary[]>("by-category")` — per-category spending
- `useSummary<MonthSummary[]>("by-month")` — monthly breakdown
- `useTransactions(1)` — first page for recent transactions

---

### TransactionsPage

Full paginated table of all transactions with filtering and sorting.

```tsx
<PageLayout>
  <TransactionFilters />
  <TransactionTable />
  <Pagination />
</PageLayout>
```

**Features:**
- Filter by date range (shadcn `Calendar` / date picker)
- Filter by category (shadcn `Select`)
- Text search on description
- Sortable columns (date, amount, category)
- Pagination (25 per page — already implemented in backend)
- Click category badge to recategorize inline (`PUT /transactions/:id`)

---

### ImportPage

Upload a CSV and see what was imported.

```tsx
<PageLayout>
  <CsvUpload />
  <ImportSummary />   {/* only shown after successful upload */}
</PageLayout>
```

**Features:**
- File validation (type check already done)
- Loading state while uploading (`isPending` from `useImport`)
- Show import result — rows imported, skipped duplicates, breakdown by category

---

## Components

### Layout

#### `Sidebar`
Left-side navigation using shadcn `sidebar.tsx` (already installed).

Links:
- Dashboard (`/`)
- Transactions (`/transactions`)
- Import (`/import`)
- Categories (`/categories`) — future

Use shadcn `Tooltip` on icons if collapsed.

---

### Dashboard

#### `TotalsSummary`
Three shadcn `Card` components side by side.

```
┌─────────────────┐  ┌─────────────────┐  ┌─────────────────┐
│   Inntekt       │  │   Utgifter      │  │   Balanse       │
│   + 42 000 kr   │  │   - 18 400 kr   │  │   + 23 600 kr   │
└─────────────────┘  └─────────────────┘  └─────────────────┘
```

- Green for income, red for expenses, neutral for balance
- Use `useSummary<Totals>("totals")`
- Add `MonthSummary` type to `types.ts`

#### `SpendingByCategoryChart`
Pie or donut chart using shadcn `chart.tsx` (wraps Recharts).

- One slice per category
- Label shows category name + percentage
- Legend below or on the side
- Data from `useSummary<CategorySummary[]>("by-category")`

#### `SpendingByMonthChart`
Bar chart — one bar per month, split into income vs expenses.

- X axis: month name
- Y axis: amount in NOK
- Two bars per month (income green, expenses red)
- Data from `useSummary<MonthSummary[]>("by-month")`
- Add `MonthSummary` type to `types.ts`:
  ```ts
  export type MonthSummary = {
    month: number;
    total: number;
    count: number;
  };
  ```

#### `RecentTransactions`
Compact shadcn `Table` showing the last 5-10 transactions.

Columns: Date | Description | Category | Amount

- Truncate long descriptions
- Color amount red/green based on sign
- Link "Se alle" to `/transactions`

---

### Transactions

#### `TransactionFilters`
Filter bar above the table using shadcn components:

- `Input` for text search
- `Select` for category filter (populated from `useCategories` — hook to add)
- `Calendar` or date range for from/to
- Filters passed as query params into `useTransactions(page, filters)`

Update `useTransactions` to accept a filters object:
```ts
export const useTransactions = (pageNumber: number, filters?: TransactionFilters) => {
  return useQuery({
    queryKey: ["transactions", pageNumber, filters],
    queryFn: () => client.get("/transactions", { params: { pageNumber, ...filters } }).then(r => r.data)
  });
};
```

#### `TransactionTable`
Full shadcn `Table` with all columns.

Columns: Date | Description | Category | Amount | Account

#### `CategoryBadge`
Inline pill showing the category name. Click to open a `Popover` or `DropdownMenu` with category options. On select, fires `PUT /transactions/:id`.

Will need:
- `useCategories` hook (`GET /categories`)
- `useUpdateTransaction` mutation hook

#### `TransactionRow`
Single row component — receives a `Transaction` and renders it. Keeps `TransactionTable` clean.

---

### Import

#### `CsvUpload`
Already mostly built. Remaining:
- Disable button while `isPending`
- Show error state if upload fails (shadcn `Alert`)
- On success, show `ImportSummary`

#### `ImportSummary`
Shows the result returned from `POST /import`:
- Total rows imported
- Skipped duplicates
- Breakdown by category (shadcn `Table` or simple list)
- Option to undo the import (`DELETE /import/batches/:id`)

---

## Types to Add (`types.ts`)

```ts
export type Totals = {
  income: number;
  expenses: number;
  balance: number;
};

export type MonthSummary = {
  month: number;
  total: number;
  count: number;
};

export type TransactionFilters = {
  from?: string;
  to?: string;
  categoryId?: number;
  search?: string;
  sortBy?: string;
  sortDir?: "asc" | "desc";
};
```

---

## Hooks to Add

| Hook | Endpoint | Notes |
|---|---|---|
| `useCategories` | `GET /categories` | For filter dropdowns and category badge |
| `useUpdateTransaction` | `PUT /transactions/:id` | For inline recategorization |
| `useDeleteImportBatch` | `DELETE /import/batches/:id` | For undo import |

---

## Build Order

Build in this order so you always have something working to look at:

1. **`TotalsSummary`** — simplest component, immediate visual payoff
2. **`Sidebar`** + layout — makes navigation feel real
3. **`SpendingByCategoryChart`** — data already flowing from `useSummary`
4. **`SpendingByMonthChart`** — same pattern, different shape
5. **`RecentTransactions`** — reuse table work from TransactionTable
6. **`TransactionsPage`** + `TransactionTable` — move existing table here
7. **`TransactionFilters`** — add filtering on top of table
8. **`CategoryBadge`** — inline editing, needs `useUpdateTransaction`
9. **`ImportSummary`** — polish the import flow

---

## Nice to Have (Later)

- Date range selector on dashboard (this month / last 3 months / all time)
- Categories management page — view/add/delete keyword rules
- Dark mode (Tailwind + shadcn already support it)
- Export filtered transactions as CSV
- Monthly budget targets with `Progress` bar per category
- Skeleton loading states using shadcn `Skeleton` instead of plain "Loading..."
