import { useMemo, useState } from "react";
import PageHeader from "../../components/ui/PageHeader";
import TotalsInline from "../../components/ui/TotalsInline";
import Button from "../../components/ui/Button";
import Card from "../../components/ui/Card";
import Table from "../../components/ui/Table";
import { useCategorias, useCreateCategoria, useTotaisCategorias } from "./hooks";
import type { Categoria, TotaisCategoriaItem } from "./types";
import { Finalidade } from "./types";
import { ApiError } from "../../lib/http";
import { currencyBRL } from "../../lib/format";
import CategoriaModal from "./CategoriaModal";

function finalidadeLabel(v: Finalidade) {
  switch (v) {
    case Finalidade.Despesa:
      return "Despesa";
    case Finalidade.Receita:
      return "Receita";
    case Finalidade.Ambas:
      return "Ambas";
    default:
      return String(v);
  }
}

export default function CategoriaPage() {
  const [ open, setOpen ] = useState(false);

  const categoriasQ = useCategorias();
  const totaisQ = useTotaisCategorias();
  const createM = useCreateCategoria();

  const rows = useMemo(() => (Array.isArray(categoriasQ.data) ? categoriasQ.data : []), [ categoriasQ.data ]);
  const totaisRows = useMemo(
    () => (totaisQ.data?.categorias && Array.isArray(totaisQ.data.categorias) ? totaisQ.data.categorias : []),
    [ totaisQ.data ]
  );

  const errorText = (err: unknown) => (err instanceof ApiError ? err.message : "Erro inesperado");

  return (
    <div className="grid gap-4">
      <PageHeader title="Categorias" action={<Button onClick={() => setOpen(true)}>Nova categoria</Button>} />

      <Card
        title="Totais por categoria"
        right={
          totaisQ.data ? (
            <TotalsInline
              receitas={totaisQ.data.totalReceitasGeral}
              despesas={totaisQ.data.totalDespesasGeral}
              saldo={totaisQ.data.saldoGeral}
            />
          ) : null
        }
      >
        {totaisQ.isLoading ? (
          <div className="text-sm text-slate-600 dark:text-slate-300">Carregando...</div>
        ) : totaisQ.isError ? (
          <div role="alert" className="text-sm text-red-600">
            {errorText(totaisQ.error)}
          </div>
        ) : (
          <Table
            rows={totaisRows}
            empty="Sem dados de totais."
            columns={[
              { key: "descricao", header: "Categoria", cell: (r: TotaisCategoriaItem) => <span className="font-medium">{r.descricao}</span> },
              { key: "totalReceitas", header: "Receitas", className: "text-right", cell: (r: TotaisCategoriaItem) => currencyBRL(r.totalReceitas) },
              { key: "totalDespesas", header: "Despesas", className: "text-right", cell: (r: TotaisCategoriaItem) => currencyBRL(r.totalDespesas) },
              { key: "saldo", header: "Saldo", className: "text-right", cell: (r: TotaisCategoriaItem) => currencyBRL(r.saldo) },
            ]}
          />
        )}
      </Card>

      <Card title="Listagem">
        {categoriasQ.isLoading ? (
          <div className="text-sm text-slate-600 dark:text-slate-300">Carregando...</div>
        ) : categoriasQ.isError ? (
          <div role="alert" className="text-sm text-red-600">
            {errorText(categoriasQ.error)}
          </div>
        ) : (
          <Table
            rows={rows}
            empty="Nenhuma categoria cadastrada."
            columns={[
              { key: "descricao", header: "Descrição", cell: (r: Categoria) => <span className="font-medium">{r.descricao}</span> },
              { key: "finalidade", header: "Finalidade", className: "w-[160px]", cell: (r: Categoria) => finalidadeLabel(r.finalidade) },
            ]}
          />
        )}
      </Card>

      <CategoriaModal
        open={open}
        onClose={() => setOpen(false)}
        busy={createM.isPending}
        error={createM.error}
        onSubmit={(payload) => createM.mutateAsync(payload)}
      />
    </div>
  );
}
