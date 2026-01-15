import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { createCategoria, getCategorias } from "./api";
import type { CategoriaCreate } from "./types";

const keys = {
  all: [ "categorias" ] as const,
};

export function useCategorias() {
  return useQuery({
    queryKey: keys.all,
    queryFn: ({ signal }) => getCategorias(signal),
  });
}

export function useCreateCategoria() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (body: CategoriaCreate) => createCategoria(body),
    onSuccess: async () => {
      await qc.invalidateQueries({ queryKey: keys.all });
    },
  });
}