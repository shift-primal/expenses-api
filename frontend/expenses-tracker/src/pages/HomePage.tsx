import { SpendingByCategory } from "@/components/dashboard/SpendingByCategory";
import { PageLayout } from "@/components/layout/PageLayout";
import { useSummary } from "@/hooks/useSummary";
import type { CategorySummary } from "@/types";

export const HomePage = () => {
  const { data, isLoading } = useSummary<CategorySummary[]>("by-category");

  if (isLoading) return <h1>Loading</h1>;
  if (!data) return null;

  return (
    <PageLayout>
      {data.map((entry: CategorySummary) => (
        <SpendingByCategory
          key={entry.category.categoryId}
          category={entry.category}
          count={entry.count}
          total={entry.total}
        />
      ))}
    </PageLayout>
  );
};
