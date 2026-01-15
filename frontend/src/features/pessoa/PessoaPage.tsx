import { useMemo, useState } from "react";
import Card from "../../components/ui/Card";
import Table from "../../components/ui/Table";
import Button from "../../components/ui/Button";
import Modal from "../../components/ui/Modal";
import Field from "../../components/ui/Field";
import { useCreatePessoa, useDeletePessoa, usePessoas, useTotaisPessoas } from "./hooks";
import type { Pessoa } from "./types";
import { ApiError } from "../../lib/http";
import { currencyBRL } from "../../lib/format";

export default function PessoaPage() {
  const [ open, setOpen ] = useState(false);
  const [ nome, setNome ] = useState("");
  const [ idade, setIdade ] = useState<string>("");

  const pessoasQ = usePessoas();
  const totaisQ = useTotaisPessoas();
  const createM = useCreatePessoa();
  const deleteM = useDeletePessoa();

  const pessoas = useMemo(() => pessoasQ.data ?? [], [ pessoasQ.data ]);

  const errorText = (err: unknown) => {
    if (err instanceof ApiError) return `${err.message} (HTTP ${err.status})`;
    return "Erro inesperado";
  };

  const onClose = () => {
    setOpen(false);
    setNome("");
    setIdade("");
  };

  const onConfirm = async () => {
    const n = nome.trim();
    const i = Number(idade);
    if (!n) return;
    if (!Number.isFinite(i) || i < 0) return;

    await createM.mutateAsync({ nome: n, idade: i });
    onClose();
  };

  const onRemove = async (p: Pessoa) => {
    const ok = window.confirm(`Excluir "${p.nome}"? Isso deve apagar as transações dela (regra do backend).`);
    if (!ok) return;
    await deleteM.mutateAsync(p.id);
  };

  return (
    <div className="grid gap-4">
      <div className="flex flex-wrap items-center justify-between gap-3">
        <div>
          <h1 className="text-lg font-semibold">Pessoas</h1>
          <p className="text-sm text-slate-600">GET/POST/DELETE /api/Pessoa + totais</p>
        </div>
        <Button onClick={() => setOpen(true)}>Nova pessoa</Button>
      </div>

      <Card title="Totais por pessoa">
        {totaisQ.isLoading ? (
          <div className="text-sm text-slate-600">Carregando...</div>
        ) : totaisQ.isError ? (
          <div role="alert" className="text-sm text-red-600">
            {errorText(totaisQ.error)}
          </div>
        ) : (
          <Table
            rows={totaisQ.data ?? []}
            empty="Sem dados de totais."
            columns={[
              { key: "nome", header: "Pessoa", cell: (r) => r.nome },
              { key: "total", header: "Total", className: "text-right", cell: (r) => currencyBRL(r.total) },
            ]}
          />
        )}
      </Card>

      <Card title="Listagem">
        {pessoasQ.isLoading ? (
          <div className="text-sm text-slate-600">Carregando...</div>
        ) : pessoasQ.isError ? (
          <div role="alert" className="text-sm text-red-600">
            {errorText(pessoasQ.error)}
          </div>
        ) : (
          <Table
            rows={pessoas}
            empty="Nenhuma pessoa cadastrada."
            columns={[
              { key: "nome", header: "Nome", cell: (r) => <span className="font-medium">{r.nome}</span> },
              { key: "idade", header: "Idade", className: "w-[120px]", cell: (r) => r.idade },
              {
                key: "acoes",
                header: "Ações",
                className: "w-[140px] text-right",
                cell: (r) => (
                  <div className="flex justify-end">
                    <Button
                      variant="danger"
                      onClick={() => onRemove(r)}
                      disabled={deleteM.isPending}
                      aria-label={`Excluir ${r.nome}`}
                    >
                      {deleteM.isPending ? "..." : "Excluir"}
                    </Button>
                  </div>
                ),
              },
            ]}
          />
        )}

        {(deleteM.isError || createM.isError) && (
          <div role="alert" className="mt-3 text-sm text-red-600">
            {deleteM.isError ? errorText(deleteM.error) : errorText(createM.error)}
          </div>
        )}
      </Card>

      <Modal open={open} title="Criar pessoa" onClose={onClose} onConfirm={onConfirm} busy={createM.isPending}>
        <div className="grid gap-3">
          <Field label="Nome" value={nome} onChange={(e) => setNome(e.target.value)} />
          <Field label="Idade" inputMode="numeric" value={idade} onChange={(e) => setIdade(e.target.value)} />
          <p className="text-xs text-slate-500">
            Regra: ao excluir pessoa, as transações dela devem ser apagadas no backend.
          </p>
        </div>
      </Modal>
    </div>
  );
}
