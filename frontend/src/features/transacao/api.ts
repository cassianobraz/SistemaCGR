import { apiFetch } from "../../lib/http";
import type { Transacao, TransacaoCreate } from "./types";

export function getTransacoes(signal?: AbortSignal) {
  return apiFetch<Transacao[]>("/api/Transacao", { signal });
}

export function createTransacao(body: TransacaoCreate) {
  return apiFetch<void>("/api/Transacao", { method: "POST", body: JSON.stringify(body) });
}
