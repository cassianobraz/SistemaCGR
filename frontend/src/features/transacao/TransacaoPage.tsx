import { useMemo, useState } from "react";
import Card from "../../components/ui/Card";
import Table from "../../components/ui/Table";
import Button from "../../components/ui/Button";
import Modal from "../../components/ui/Modal";
import Field from "../../components/ui/Field";
import { useCreateTransacao, useTransacoes } from "./hooks";
import { usePessoas } from "../pessoa/hooks";
import { useCategorias } from "../categoria/hooks";
import { ApiError } from "../../lib/http";
import { currencyBRL } from "../../lib/format";

export default function TransacaoPage() {
  const [ open, setOpen ] = useState(false);

  const [ descricao, setDescricao ] = useState("");
  const [ valor, setValor ] = useState<string>("");
  const [ data, setData ] = useState<string>(() => new Date().toISOString().slice(0, 10));
  const [ pessoaId, setPessoaId ] = useState("");
  const [ categoriaId, setCategoriaId ] = useState("");

  const transacoesQ = useTransacoes();
  const createM = useCreateTransacao();

  const pessoasQ = usePessoas();
  const categoriasQ = useCategorias();

  const pessoas = useMemo(() => pessoasQ.data ?? [], [ pessoasQ.data ]);
  const categorias = useMemo(() => (categoriasQ.data ?? []).filter((c) => c.nivel === 0), [ categoriasQ.data ]);

  const errorText = (err: unknown) => {
    if (err instanceof ApiError) return `${err.message} (HTTP ${err.status})`;
    return "Erro inesperado";
  };

  const onClose = () => {
    setOpen(false);
    setDescricao("");
    setValor("");
    setPessoaId("");
    setCategoriaId("");
    setData(new Date().toISOString().slice(0, 10));
  };

  const onConfirm = async () => {
    const d = descricao.trim();
    const v = Number(valor);
    if (!d) return;
    if (!Number.isFinite(v) || v <= 0) return;
    if (!pessoaId || !categoriaId) return;

    await createM.mutateAsync({
      descricao: d,
      valor: v,
      data: new Date(data).toISOString(),
      pessoaId,
      categoriaId,
    });

    onClose();
  };

  return (
    <div className="grid gap-4">
      <div className="flex flex-wrap items-center justify-between gap-3">
        <div>
          <h1 className="text-lg font-semibold">Transações</h1>
          <p className="text-sm text-slate-600">GET/POST /api/Transacao</p>
        </div>
        <Button onClick={() => setOpen(true)} disabled={pessoas.length === 0 || categorias.length === 0}>
          Nova transação
        </Button>
      </div>

      {(pessoasQ.isLoading || categoriasQ.isLoading) && (
        <div className="text-sm text-slate-600">Carregando base (pessoas/categorias)...</div>
      )}

      {(pessoas.length === 0 || categorias.length === 0) && !pessoasQ.isLoading && !categoriasQ.isLoading && (
        <Card title="Atenção">
          <p className="text-sm text-slate-700">
            Para criar transações, você precisa ter ao menos <b>1 pessoa</b> e <b>1 categoria</b>.
          </p>
        </Card>
      )}

      <Card title="Listagem">
        {transacoesQ.isLoading ? (
          <div className="text-sm text-slate-600">Carregando...</div>
        ) : transacoesQ.isError ? (
          <div role="alert" className="text-sm text-red-600">
            {errorText(transacoesQ.error)}
          </div>
        ) : (
          <Table
            rows={transacoesQ.data ?? []}
            empty="Nenhuma transação cadastrada."
            columns={[
              { key: "descricao", header: "Descrição", cell: (r) => <span className="font-medium">{r.descricao}</span> },
              {
                key: "valor",
                header: "Valor",
                className: "w-[160px] text-right",
                cell: (r) => currencyBRL(r.valor),
              },
              {
                key: "data",
                header: "Data",
                className: "w-[140px]",
                cell: (r) => new Date(r.data).toLocaleDateString("pt-BR"),
              },
              { key: "pessoaId", header: "PessoaId", className: "w-[220px] text-slate-500", cell: (r) => r.pessoaId },
              {
                key: "categoriaId",
                header: "CategoriaId",
                className: "w-[220px] text-slate-500",
                cell: (r) => r.categoriaId,
              },
            ]}
          />
        )}
      </Card>

      <Modal open={open} title="Criar transação" onClose={onClose} onConfirm={onConfirm} busy={createM.isPending}>
        <div className="grid gap-3">
          <Field label="Descrição" value={descricao} onChange={(e) => setDescricao(e.target.value)} />

          <Field
            label="Valor"
            inputMode="decimal"
            value={valor}
            onChange={(e) => setValor(e.target.value)}
            placeholder="Ex: 120.50"
          />

          <label className="grid gap-1">
            <span className="text-xs font-medium text-slate-700">Data</span>
            <input
              type="date"
              value={data}
              onChange={(e) => setData(e.target.value)}
              className="w-full rounded-md border border-slate-300 px-3 py-2 text-sm focus:border-slate-400 focus:outline-none focus:ring-2 focus:ring-slate-200"
            />
          </label>

          <label className="grid gap-1">
            <span className="text-xs font-medium text-slate-700">Pessoa</span>
            <select
              value={pessoaId}
              onChange={(e) => setPessoaId(e.target.value)}
              className="w-full rounded-md border border-slate-300 px-3 py-2 text-sm focus:border-slate-400 focus:outline-none focus:ring-2 focus:ring-slate-200"
            >
              <option value="">Selecione...</option>
              {pessoas.map((p) => (
                <option key={p.id} value={p.id}>
                  {p.nome}
                </option>
              ))}
            </select>
          </label>

          <label className="grid gap-1">
            <span className="text-xs font-medium text-slate-700">Categoria</span>
            <select
              value={categoriaId}
              onChange={(e) => setCategoriaId(e.target.value)}
              className="w-full rounded-md border border-slate-300 px-3 py-2 text-sm focus:border-slate-400 focus:outline-none focus:ring-2 focus:ring-slate-200"
            >
              <option value="">Selecione...</option>
              {categorias.map((c) => (
                <option key={c.id} value={c.id}>
                  {c.nome}
                </option>
              ))}
            </select>
          </label>

          {createM.isError && (
            <div role="alert" className="text-sm text-red-600">
              {errorText(createM.error)}
            </div>
          )}
        </div>
      </Modal>
    </div>
  );
}
