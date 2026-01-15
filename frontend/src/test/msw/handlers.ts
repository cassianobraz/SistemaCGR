import { http, HttpResponse } from "msw";

type Categoria = { id: string; descricao: string; finalidade: number };
type Pessoa = { id: string; nome: string; idade: number };
type Transacao = {
  id: string;
  descricao: string;
  valor: number;
  tipo: number;
  pessoaId: string;
  categoriaId: string;
};

let categorias: Categoria[] = [
  { id: "c1", descricao: "Mercado", finalidade: 1 },
  { id: "c2", descricao: "Salário", finalidade: 2 },
];

let pessoas: Pessoa[] = [
  { id: "p1", nome: "Maria", idade: 30 },
  { id: "p2", nome: "João", idade: 25 },
];

let transacoes: Transacao[] = [
  { id: "t1", descricao: "Inicial", valor: 10, tipo: 1, pessoaId: "p1", categoriaId: "c1" },
];

export const handlers = [
  http.get(/\/api\/Pessoa$/, () => HttpResponse.json(pessoas)),
  http.post(/\/api\/Pessoa$/, async ({ request }) => {
    const body = (await request.json()) as { nome: string; idade: number };
    const nova: Pessoa = { id: crypto.randomUUID(), nome: body.nome, idade: body.idade };
    pessoas = [ nova, ...pessoas ];
    return HttpResponse.json(nova, { status: 201 });
  }),
  http.get(/\/api\/Pessoa\/totais-pessoas$/, () =>
    HttpResponse.json({
      result: pessoas.map((p) => ({ id: p.id, nome: p.nome, totalReceitas: 0, totalDespesas: 0, saldo: 0 })),
      totalReceitasGeral: 0,
      totalDespesasGeral: 0,
      saldoGeral: 0,
    })
  ),
  http.delete(/\/api\/Pessoa\/[^/]+$/, () => new HttpResponse(null, { status: 204 })),

  http.get(/\/api\/Categoria$/, () => HttpResponse.json(categorias)),
  http.post(/\/api\/Categoria$/, async ({ request }) => {
    const body = (await request.json()) as { descricao: string; finalidade: number };
    const nova: Categoria = { id: crypto.randomUUID(), descricao: body.descricao, finalidade: body.finalidade };
    categorias = [ nova, ...categorias ];
    return HttpResponse.json(nova, { status: 201 });
  }),
  http.get(/\/api\/Categoria\/totais-categorias$/, () =>
    HttpResponse.json({
      categorias: categorias.map((c) => ({ id: c.id, descricao: c.descricao, totalReceitas: 0, totalDespesas: 0, saldo: 0 })),
      totalReceitasGeral: 0,
      totalDespesasGeral: 0,
      saldoGeral: 0,
    })
  ),

  http.get(/\/api\/Transacao$/, () => HttpResponse.json(transacoes)),
  http.post(/\/api\/Transacao$/, async ({ request }) => {
    const body = (await request.json()) as Omit<Transacao, "id">;
    const nova: Transacao = { id: crypto.randomUUID(), ...body };
    transacoes = [ nova, ...transacoes ];
    return HttpResponse.json(nova, { status: 201 });
  }),
];
