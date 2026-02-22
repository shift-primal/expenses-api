import { SpendingByCategoryPieChart } from "@/components/dashboard/SpendingByCategoryPieChart";
import { TotalsSummaryView } from "@/components/dashboard/TotalsSummaryView";
import { PageLayout } from "@/components/layout/PageLayout";
import { useSummary } from "@/hooks/useSummary";
import type { TotalsSummary, CategorySummary } from "@/types";

export const HomePage = () => {
  const { data: totals, isLoading: totalsLoading } =
    useSummary<TotalsSummary>("totals");
  const { data: byCategory, isLoading: categoryLoading } =
    useSummary<CategorySummary>("by-category");
  const { data: byMonth, isLoading: monthLoading } =
    useSummary<any>("by-month");

  if (totalsLoading || !totals || categoryLoading || !byCategory)
    return <h1>Loading</h1>;

  return (
    <PageLayout>
      <div>
        <TotalsSummaryView data={totals} />
        <SpendingByCategoryPieChart data={byCategory} />
      </div>
    </PageLayout>
  );
};
