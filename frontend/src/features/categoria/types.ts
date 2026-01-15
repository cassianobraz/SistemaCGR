export enum Finalidade {
  Despesa = 1,
  Receita = 2,
  Ambas = 3,
}

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
