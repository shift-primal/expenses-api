import { client } from "@api/client";
import { useMutation } from "@tanstack/react-query";

export const useImport = () => {
  return useMutation({
    mutationFn: (file: File) => {
      const formData = new FormData();
      formData.append("csv", file);
      return client.post("/import", formData);
    }
  });
};
