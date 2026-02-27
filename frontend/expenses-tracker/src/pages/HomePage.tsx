import { ByMonthChart } from "@charts/LineByMonthChart";
import { PieByCategoryChart } from "@charts/PieByCategoryChart";
import { TotalsSummary } from "@components/dashboard/TotalsSummary";
import { PageLayout } from "@layout/PageLayout";
import { useSummary } from "@hooks/useSummary";
import type { Totals, CategorySummary, MonthSummary } from "@types";

export const HomePage = () => {
  const { data: totals, isLoading: totalsLoading } =
    useSummary<Totals>("totals");
  const { data: byCategory, isLoading: categoryLoading } =
    useSummary<CategorySummary[]>("by-category");
  const { data: byMonth, isLoading: monthLoading } =
    useSummary<MonthSummary[]>("by-month");
  const isLoading =
    totalsLoading ||
    categoryLoading ||
    monthLoading ||
    byCategory?.length == 0 ||
    byMonth?.length == 0;

  if (isLoading || !totals || !byCategory || !byMonth)
    return (
      <PageLayout>
        <h1>Loading...</h1>
      </PageLayout>
    );

  return (
    <PageLayout>
      <div className="flex flex-1 flex-col">
        <div className="@container/main flex flex-1 flex-col gap-2 p-4">
          <div className="flex flex-col gap-4 py-4 md:gap-6 md:py-6">
            <TotalsSummary data={totals} />
            <div className="grid grid-cols-1 gap-4 px-4 *:data-[slot=card]:bg-linear-to-t *:data-[slot=card]:shadow-xs lg:px-6  @5xl/main:grid-cols-2">
              <PieByCategoryChart
                categoryData={byCategory}
                monthData={byMonth}
              />
              <ByMonthChart data={byMonth} />
            </div>
          </div>
        </div>
      </div>
    </PageLayout>
  );
};
