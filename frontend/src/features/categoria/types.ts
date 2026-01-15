export const Finalidade = {
  Despesa: 1,
  Receita: 2,
  Ambas: 3,
} as const;

export type Finalidade = typeof Finalidade[ keyof typeof Finalidade ];

export type Categoria = {
  id: string;
  descricao: string;
  finalidade: Finalidade;
};

export type CategoriaCreate = {
  descricao: string;
  finalidade: Finalidade;
};

export type TotaisCategoriaItem = {
  id: string;
  descricao: string;
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
};

export type TotaisCategoriasResponse = {
  categorias: TotaisCategoriaItem[];
  totalReceitasGeral: number;
  totalDespesasGeral: number;
  saldoGeral: number;
};
