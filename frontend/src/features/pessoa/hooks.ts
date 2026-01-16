import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { createPessoa, deletePessoa, getPessoas, getTotaisPessoas } from "./api";
import type { PessoaCreate } from "./types";

const keys = {
  all: [ "pessoas" ] as const,
  totais: [ "pessoas", "totais" ] as const,
};

export function usePessoas() {
  return useQuery({
    queryKey: keys.all,
    queryFn: ({ signal }) => getPessoas(signal),
  });
}

export function useTotaisPessoas() {
  return useQuery({
    queryKey: keys.totais,
    queryFn: ({ signal }) => getTotaisPessoas(signal),
  });
}

export function useCreatePessoa() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (body: PessoaCreate) => createPessoa(body),
    onSuccess: async () => {
      await Promise.all([ qc.invalidateQueries({ queryKey: keys.all }), qc.invalidateQueries({ queryKey: keys.totais }) ]);
    },
  });
}

export function useDeletePessoa() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (id: string) => deletePessoa(id),
    onSuccess: async () => {
      await Promise.all([ qc.invalidateQueries({ queryKey: keys.all }), qc.invalidateQueries({ queryKey: keys.totais }) ]);
    },
  });
}
