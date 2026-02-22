import type { TotalsSummary } from "@/types";
import { TotalsCard } from "./TotalsCard";

export const TotalsSummaryView = ({ data }: { data: TotalsSummary }) => {
  return (
    <div className="flex gap-8">
      {Object.entries(data).map(([label, value]) => (
        <TotalsCard key={label} label={label} value={value as number} />
      ))}
    </div>
  );
};
