import { apiFetch } from "../../lib/http";
import type { Pessoa, PessoaCreate, TotalPessoas } from "./types";

export function getPessoas(signal?: AbortSignal) {
  return apiFetch<Pessoa[]>("/api/Pessoa", { signal });
}

export function createPessoa(body: PessoaCreate) {
  return apiFetch<void>("/api/Pessoa", { method: "POST", body: JSON.stringify(body) });
}

export function deletePessoa(id: string) {
  return apiFetch<void>(`/api/Pessoa/${encodeURIComponent(id)}`, { method: "DELETE" });
}

export function getTotaisPessoas(signal?: AbortSignal) {
  return apiFetch<TotalPessoas[]>("/api/Pessoa/totais-pessoas", { signal });
}
