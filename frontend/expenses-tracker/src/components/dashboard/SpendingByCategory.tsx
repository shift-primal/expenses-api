import type { CategorySummary } from "@/types";

export const SpendingByCategory = ({
  category,
  count,
  total
}: CategorySummary) => {
  return (
    <>
      <span>{category.name}</span>
      <span>{count}</span>
      <span>{total}</span>
    </>
  );
};
