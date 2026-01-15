export type Transacao = {
  id: string;
  descricao: string;
  valor: number;
  data: string;
  pessoaId: string;
  categoriaId: string;
  tipo?: "Receita" | "Despesa" | string;
};

export type TransacaoCreate = {
  descricao: string;
  valor: number;
  data: string;
  pessoaId: string;
  categoriaId: string;
  tipo?: "Receita" | "Despesa";
};
