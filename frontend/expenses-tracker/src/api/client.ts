import createClient from "openapi-fetch";
import type { paths } from "./openapi.ts";

export const apiBaseUrl = "http://localhost:5071";

export const client = createClient<paths>({
  baseUrl: apiBaseUrl,
  credentials: "include",
});
