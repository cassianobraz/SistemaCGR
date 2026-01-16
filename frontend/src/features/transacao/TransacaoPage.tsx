import { useMemo, useState } from "react";
import PageHeader from "../../components/ui/PageHeader";
import Button from "../../components/ui/Button";
import Card from "../../components/ui/Card";
import Table from "../../components/ui/Table";
import { ApiError } from "../../lib/http";
import { currencyBRL } from "../../lib/format";
import { useCategorias } from "../categoria/hooks";
import { usePessoas } from "../pessoa/hooks";
import { useCreateTransacao, useTransacoes } from "./hooks";
import type { Transacao } from "./types";
import { TipoTransacao } from "./types";
import TransacaoModal from "./TransacaoModal";
import type { Pessoa } from "../pessoa/types";
import type { Categoria } from "../categoria/types";

function tipoLabel(v: TipoTransacao) {
  switch (v) {
    case TipoTransacao.Despesa:
      return "Despesa";
    case TipoTransacao.Receita:
      return "Receita";
    default:
      return String(v);
  }
}

export default function TransacaoPage() {
  const [ open, setOpen ] = useState(false);

  const transacoesQ = useTransacoes();
  const createM = useCreateTransacao();

  const pessoasQ = usePessoas();
  const categoriasQ = useCategorias();

  const transacoes = useMemo(
    () => (Array.isArray(transacoesQ.data) ? transacoesQ.data : []),
    [ transacoesQ.data ]
  );

  const pessoas = useMemo(
    () => (Array.isArray(pessoasQ.data) ? pessoasQ.data : []),
    [ pessoasQ.data ]
  );

  const categorias = useMemo(
    () => (Array.isArray(categoriasQ.data) ? categoriasQ.data : []),
    [ categoriasQ.data ]
  );

  const pessoaById = useMemo(() => {
    const map = new Map<string, Pessoa>();
    for (const p of pessoas) map.set(p.id, p);
    return map;
  }, [ pessoas ]);

  const categoriaById = useMemo(() => {
    const map = new Map<string, Categoria>();
    for (const c of categorias) map.set(c.id, c);
    return map;
  }, [ categorias ]);

  const errorText = (err: unknown) =>
    err instanceof ApiError ? err.message : "Erro inesperado";

  const isLoadingAny = transacoesQ.isLoading || pessoasQ.isLoading || categoriasQ.isLoading;
  const isErrorAny = transacoesQ.isError || pessoasQ.isError || categoriasQ.isError;

  const errorAny =
    (transacoesQ.isError && transacoesQ.error) ||
    (pessoasQ.isError && pessoasQ.error) ||
    (categoriasQ.isError && categoriasQ.error);

  return (
    <div className="grid gap-4">
      <PageHeader
        title="Transações"
        action={<Button onClick={() => setOpen(true)}>Nova transação</Button>}
      />

      <Card title="Listagem">
        {isLoadingAny ? (
          <div className="text-sm text-slate-600 dark:text-slate-300">
            Carregando...
          </div>
        ) : isErrorAny ? (
          <div role="alert" className="text-sm text-red-600">
            {errorText(errorAny)}
          </div>
        ) : (
          <Table
            rows={transacoes}
            empty="Nenhuma transação cadastrada."
            columns={[
              {
                key: "descricao",
                header: "Descrição",
                cell: (r: Transacao) => (
                  <span className="font-medium">{r.descricao}</span>
                ),
              },
              {
                key: "tipo",
                header: "Tipo",
                className: "w-[140px]",
                cell: (r: Transacao) => tipoLabel(r.tipo),
              },
              {
                key: "valor",
                header: "Valor",
                className: "w-[160px] text-right",
                cell: (r: Transacao) => currencyBRL(r.valor),
              },
              {
                key: "pessoa",
                header: "Pessoa",
                className: "w-[220px]",
                cell: (r: Transacao) => pessoaById.get(r.pessoaId)?.nome ?? "—",
              },
              {
                key: "categoria",
                header: "Categoria",
                className: "w-[220px]",
                cell: (r: Transacao) => categoriaById.get(r.categoriaId)?.descricao ?? "—",
              },
            ]}
          />
        )}
      </Card>

      <TransacaoModal
        open={open}
        onClose={() => setOpen(false)}
        busy={createM.isPending}
        error={createM.error}
        pessoas={pessoas}
        categorias={categorias}
        onSubmit={(payload) => createM.mutateAsync(payload)}
      />
    </div>
  );
}
