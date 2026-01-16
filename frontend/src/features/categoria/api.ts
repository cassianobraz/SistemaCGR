import { apiFetch } from "../../lib/http";
import type { Categoria, CategoriaCreate, TotaisCategoriasResponse } from "./types";

export function getCategorias(signal?: AbortSignal) {
  return apiFetch<Categoria[]>("/api/Categoria", { signal });
}

export function createCategoria(body: CategoriaCreate) {
  return apiFetch<void>("/api/Categoria", {
    method: "POST",
    body: JSON.stringify(body),
  });
}

export function getTotaisCategorias(signal?: AbortSignal) {
  return apiFetch<TotaisCategoriasResponse>("/api/Categoria/totais-categorias", { signal });
}
