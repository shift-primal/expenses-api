import { client } from "@api/client";
import type { Transaction } from "@types";
import { useQuery } from "@tanstack/react-query";

export const useTransactions = (pageNumber: number) => {
  return useQuery<Transaction[]>({
    queryKey: ["transactions", pageNumber],
    queryFn: () =>
      client.get(`/transactions?pageNumber=${pageNumber}`).then((r) => r.data)
  });
};
