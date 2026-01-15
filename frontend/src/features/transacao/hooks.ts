import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { createTransacao, getTransacoes } from "./api";
import type { TransacaoCreate } from "./types";

const keys = { all: [ "transacoes" ] as const };

export function useTransacoes() {
  return useQuery({
    queryKey: keys.all,
    queryFn: ({ signal }) => getTransacoes(signal),
  });
}

export function useCreateTransacao() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (body: TransacaoCreate) => createTransacao(body),
    onSuccess: async () => {
      await qc.invalidateQueries({ queryKey: keys.all });
      await qc.invalidateQueries({ queryKey: [ "pessoas", "totais" ] });
      await qc.invalidateQueries({ queryKey: [ "categorias", "totais" ] });
    },
  });
}
