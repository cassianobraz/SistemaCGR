export type Pessoa = {
  id: string;
  nome: string;
  idade: number;
};

export type PessoaCreate = {
  nome: string;
  idade: number;
};

export type TotaisPessoaItem = {
  id: string;
  nome: string;
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
};

export type TotaisPessoasResponse = {
  result: TotaisPessoaItem[];
  totalReceitasGeral: number;
  totalDespesasGeral: number;
  saldoGeral: number;
};