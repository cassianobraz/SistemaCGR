import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { createCategoria, getCategorias, getTotaisCategorias } from "./api";
import type { CategoriaCreate } from "./types";

const keys = {
  all: [ "categorias" ] as const,
  totais: [ "categorias", "totais" ] as const,
};

export function useCategorias() {
  return useQuery({
    queryKey: keys.all,
    queryFn: ({ signal }) => getCategorias(signal),
  });
}

export function useTotaisCategorias() {
  return useQuery({
    queryKey: keys.totais,
    queryFn: ({ signal }) => getTotaisCategorias(signal),
  });
}

export function useCreateCategoria() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (body: CategoriaCreate) => createCategoria(body),
    onSuccess: async () => {
      await Promise.all([ qc.invalidateQueries({ queryKey: keys.all }), qc.invalidateQueries({ queryKey: keys.totais }) ]);
    },
  });
}
