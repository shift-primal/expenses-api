import type { components } from "@/api/auto_types";

export type Transaction = components["schemas"]["Transaction"];
export type Category = components["schemas"]["Category"];
export type CategoryRule = components["schemas"]["CategoryRule"];

export type CategorySummary = {
  category: { categoryId: number; name: string };
  total: number;
  count: number;
};
