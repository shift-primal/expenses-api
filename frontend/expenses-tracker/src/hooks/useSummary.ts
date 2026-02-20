import { client } from "@/api/client";
import { useQuery } from "@tanstack/react-query";

export const useSummary = <T>(filter: string) => {
  return useQuery<T>({
    queryKey: ["summary", filter],
    queryFn: () => client.get<T>(`/summary/${filter}`).then((r) => r.data)
  });
};
