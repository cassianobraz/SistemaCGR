// src/features/categoria/api.ts
import { apiFetch } from "../../lib/http";
import type { Categoria, CategoriaCreate } from "./types";

export function getCategorias(signal?: AbortSignal) {
  return apiFetch<Categoria[]>("/api/Categoria", { signal });
}

export function createCategoria(body: CategoriaCreate) {
  return apiFetch<void>("/api/Categoria", {
    method: "POST",
    body: JSON.stringify(body),
  });
}
