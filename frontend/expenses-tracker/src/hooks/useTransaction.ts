import { client } from "@/api/client";
import { useQuery } from "@tanstack/react-query";

export const useTransactions = (pageNumber: number) => {
  return useQuery({
    queryKey: ["transactions", pageNumber],
    queryFn: () => client.get(`/transactions?pageNumber=${pageNumber}`)
  });
};
