import { clsx, type ClassValue } from "clsx";
import { twMerge } from "tailwind-merge";
import type { CategorySummary, MonthSummary } from "@/types";

const cn = (...inputs: ClassValue[]) => twMerge(clsx(inputs));

const fmt = (date: string) =>
  new Date(`${date}-01`).toLocaleDateString("nb-NO", {
    month: "short",
    year: "2-digit"
  });

const getDateRange = (summary: MonthSummary[]) => {
  const min = summary.reduce((a, b) => (a.date < b.date ? a : b)).date;
  const max = summary.reduce((a, b) => (a.date > b.date ? a : b)).date;

  return { min: fmt(min), max: fmt(max) };
};

const getMostExpensiveCategory = (data: CategorySummary[]) => {
  const mostExpensiveCategory = data.reduce((a, b) =>
    Math.abs(a.total) > Math.abs(b.total) ? a : b
  );

  return {
    mostExpensiveCategoryName: mostExpensiveCategory.category.name,
    mostExpensiveCategoryTotal: Math.abs(mostExpensiveCategory.total),
    mostExpensiveCategoryCount: mostExpensiveCategory.count
  };
};

const getMostExpensiveMonth = (summary: MonthSummary[]) => {
  const mostExpensiveMonth = summary.reduce((p, c) =>
    p.total < c.total ? p : c
  );

  return {
    expensiveMonthDate: fmt(mostExpensiveMonth.date),
    expensiveMonthTotal: Math.abs(mostExpensiveMonth.total)
  };
};

export { getDateRange, getMostExpensiveCategory, getMostExpensiveMonth, cn };
