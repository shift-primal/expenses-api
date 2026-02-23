import type { components } from "@/api/auto_types";

export type Transaction = components["schemas"]["Transaction"];

export type Totals = {
  income: number;
  expenses: number;
  balance: number;
};

export type CategorySummary = {
  category: { categoryId: number; name: string };
  total: number;
  count: number;
};

export type Category = components["schemas"]["Category"];

export type CategoryRule = components["schemas"]["CategoryRule"];

export type MonthSummary = {
  date: string;
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
