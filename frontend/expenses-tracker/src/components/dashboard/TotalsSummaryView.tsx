import { useSummary } from "@/hooks/useSummary";
import type { TotalsSummary } from "@/types";
import { TotalsCard } from "./TotalsCard";

export const TotalsSummaryView = () => {
  const { data, isLoading } = useSummary<TotalsSummary>("totals");

  if (isLoading) return <h1>Loading</h1>;
  if (!data) return null;

  return (
    <div className="flex gap-8">
      {Object.entries(data).map(([label, value]) => (
        <TotalsCard key={label} label={label} value={value as number} />
      ))}
    </div>
  );
};
