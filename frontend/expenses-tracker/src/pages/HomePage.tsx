import { ByCategoryChart } from "@/components/dashboard/ByCategoryChart";
import { ByMonthChart } from "@/components/dashboard/ByMonthChart";
import { TotalsSummary } from "@/components/dashboard/TotalsCards";
import { PageLayout } from "@/components/layout/PageLayout";
import { useSummary } from "@/hooks/useSummary";
import type { Totals, CategorySummary, MonthSummary } from "@/types";

export const HomePage = () => {
  const { data: totals, isLoading: totalsLoading } =
    useSummary<Totals>("totals");
  const { data: byCategory, isLoading: categoryLoading } =
    useSummary<CategorySummary[]>("by-category");
  const { data: byMonth, isLoading: monthLoading } =
    useSummary<MonthSummary[]>("by-month");

  if (
    totalsLoading ||
    !totals ||
    categoryLoading ||
    !byCategory ||
    monthLoading ||
    !byMonth
  )
    return <h1>Loading</h1>;

  return (
    <PageLayout>
      <div className="flex flex-1 flex-col gap-4 p-4">
        <TotalsSummary data={totals} />
        <ByCategoryChart data={byCategory} monthData={byMonth} />
        <ByMonthChart data={byMonth} />
      </div>
    </PageLayout>
  );
};
