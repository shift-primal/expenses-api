import { client } from "@api/client";
import type { Transaction, TransactionFilters } from "@types";
import { useQuery } from "@tanstack/react-query";

export const useTransactions = (
  page: number,
  {
    filters
  }: {
    filters: TransactionFilters;
  }
) => {
  return useQuery<Transaction[]>({
    queryKey: ["transactions", page, filters],
    queryFn: () =>
      client
        .get("/transactions", { params: { page: page, ...filters } })
        .then((r) => r.data)
  });
};
