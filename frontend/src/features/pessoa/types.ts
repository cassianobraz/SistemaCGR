export type Pessoa = {
  id: string;
  nome: string;
  idade: number;
};

export type PessoaCreate = {
  nome: string;
  idade: number;
};

export type TotalPessoas = {
  pessoaId: string;
  nome: string;
  total: number;
};
