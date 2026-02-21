import { TotalsSummaryView } from "@/components/dashboard/TotalsSummaryView";
import { PageLayout } from "@/components/layout/PageLayout";

export const HomePage = () => {
  return (
    <PageLayout>
      <div>
        <TotalsSummaryView />
      </div>
    </PageLayout>
  );
};
