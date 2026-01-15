import { useMemo, useState } from "react";
import PageHeader from "../../components/ui/PageHeader";
import TotalsInline from "../../components/ui/TotalsInline";
import Button from "../../components/ui/Button";
import Card from "../../components/ui/Card";
import Table from "../../components/ui/Table";
import { currencyBRL } from "../../lib/format";
import { ApiError } from "../../lib/http";
import { useCreatePessoa, useDeletePessoa, usePessoas, useTotaisPessoas } from "./hooks";
import type { Pessoa, TotaisPessoaItem } from "./types";
import PessoaModal from "./PessoaModal";

export default function PessoaPage() {
  const [ open, setOpen ] = useState(false);

  const pessoasQ = usePessoas();
  const totaisQ = useTotaisPessoas();
  const createM = useCreatePessoa();
  const deleteM = useDeletePessoa();

  const pessoas = useMemo(() => (Array.isArray(pessoasQ.data) ? pessoasQ.data : []), [ pessoasQ.data ]);
  const totaisRows = useMemo(
    () => (totaisQ.data?.result && Array.isArray(totaisQ.data.result) ? totaisQ.data.result : []),
    [ totaisQ.data ]
  );

  const errorText = (err: unknown) => (err instanceof ApiError ? err.message : "Erro inesperado");

  const onRemove = async (p: Pessoa) => {
    const ok = window.confirm(`Excluir "${p.nome}"? Isso apaga as transações dessa pessoa.`);
    if (!ok) return;
    await deleteM.mutateAsync(p.id);
  };

  return (
    <div className="grid gap-4">
      <PageHeader title="Pessoas" action={<Button onClick={() => setOpen(true)}>Nova pessoa</Button>} />

      <Card
        title="Totais por pessoa"
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
              { key: "nome", header: "Pessoa", cell: (r: TotaisPessoaItem) => <span className="font-medium">{r.nome}</span> },
              { key: "totalReceitas", header: "Receitas", className: "text-right", cell: (r: TotaisPessoaItem) => currencyBRL(r.totalReceitas) },
              { key: "totalDespesas", header: "Despesas", className: "text-right", cell: (r: TotaisPessoaItem) => currencyBRL(r.totalDespesas) },
              { key: "saldo", header: "Saldo", className: "text-right", cell: (r: TotaisPessoaItem) => currencyBRL(r.saldo) },
            ]}
          />
        )}
      </Card>

      <Card title="Listagem">
        {pessoasQ.isLoading ? (
          <div className="text-sm text-slate-600 dark:text-slate-300">Carregando...</div>
        ) : pessoasQ.isError ? (
          <div role="alert" className="text-sm text-red-600">
            {errorText(pessoasQ.error)}
          </div>
        ) : (
          <Table
            rows={pessoas}
            empty="Nenhuma pessoa cadastrada."
            columns={[
              { key: "nome", header: "Nome", cell: (r: Pessoa) => <span className="font-medium">{r.nome}</span> },
              { key: "idade", header: "Idade", className: "w-[120px]", cell: (r: Pessoa) => r.idade },
              {
                key: "acoes",
                header: "Ações",
                className: "w-[140px] text-right",
                cell: (r: Pessoa) => (
                  <div className="flex justify-end">
                    <Button variant="danger" onClick={() => onRemove(r)} disabled={deleteM.isPending}>
                      {deleteM.isPending ? "..." : "Excluir"}
                    </Button>
                  </div>
                ),
              },
            ]}
          />
        )}
      </Card>

      <PessoaModal
        open={open}
        onClose={() => setOpen(false)}
        busy={createM.isPending}
        error={createM.error}
        onSubmit={(payload) => createM.mutateAsync(payload)}
      />
    </div>
  );
}
