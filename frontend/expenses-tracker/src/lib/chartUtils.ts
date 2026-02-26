import type { Category, CategorySummary, MonthSummary } from "@types";

const fmtDateToString = (date: string) =>
  new Date(`${date}-01`).toLocaleDateString("nb-NO", {
    month: "short",
    year: "2-digit"
  });

const fmtDate = (date: string) => new Date(`${date}-01`);

const fmtCurrency = (amount: string | number) => {
  return new Intl.NumberFormat("nb-NO", {
    style: "currency",
    currency: "NOK"
  }).format(typeof amount === "string" ? parseFloat(amount) : amount);
};

const getCategoryName = (c: Category) => c.name;

const getDateRange = (summary: MonthSummary[]) => {
  const min = summary.reduce((a, b) => (a.date < b.date ? a : b)).date;
  const max = summary.reduce((a, b) => (a.date > b.date ? a : b)).date;
  const diffInDays = Math.round(
    (fmtDate(max).getTime() - fmtDate(min).getTime()) / (1000 * 60 * 60 * 24)
  );

  return { min: fmtDateToString(min), max: fmtDateToString(min), diffInDays };
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
    expensiveMonthDate: fmtDateToString(mostExpensiveMonth.date),
    expensiveMonthTotal: Math.abs(mostExpensiveMonth.total)
  };
};

export {
  getDateRange,
  getMostExpensiveCategory,
  getMostExpensiveMonth,
  fmtCurrency,
  getCategoryName
};
