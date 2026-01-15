export enum TipoTransacao {
  Despesa = 1,
  Receita = 2,
}

export type TransacaoCreate = {
  descricao: string;
  valor: number;
  tipo: TipoTransacao;
  categoriaId: string;
  pessoaId: string;
};

export type Transacao = {
  id: string;
  descricao: string;
  valor: number;
  tipo: TipoTransacao;
  categoriaId: string;
  pessoaId: string;
};
