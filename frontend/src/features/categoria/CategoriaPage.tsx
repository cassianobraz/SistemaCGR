import { useMemo, useState } from "react";
import Card from "../../components/ui/Card";
import Table from "../../components/ui/Table";
import Button from "../../components/ui/Button";
import Modal from "../../components/ui/Modal";
import Field from "../../components/ui/Field";
import { useCategorias, useCreateCategoria } from "./hooks";
import { ApiError } from "../../lib/http";
import type { Categoria } from "./types";
import { Finalidade } from "./types";

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
  const [ descricao, setDescricao ] = useState("");
  const [ finalidade, setFinalidade ] = useState<Finalidade>(Finalidade.Despesa);

  const categoriasQ = useCategorias();
  const createM = useCreateCategoria();

  const rows = useMemo(() => (Array.isArray(categoriasQ.data) ? categoriasQ.data : []), [ categoriasQ.data ]);

  const errorText = (err: unknown) => {
    if (err instanceof ApiError) return `${err.message} (HTTP ${err.status})`;
    return "Erro inesperado";
  };

  const onClose = () => {
    setOpen(false);
    setDescricao("");
    setFinalidade(Finalidade.Despesa);
  };

  const onConfirm = async () => {
    const d = descricao.trim();
    if (!d) return;

    await createM.mutateAsync({ descricao: d, finalidade });
    onClose();
  };

  return (
    <div className="grid gap-4">
      <div className="flex flex-wrap items-center justify-between gap-3">
        <div>
          <h1 className="text-lg font-semibold">Categorias</h1>
          <p className="text-sm text-slate-600">GET/POST /api/Categoria</p>
        </div>
        <Button onClick={() => setOpen(true)}>Nova categoria</Button>
      </div>

      <Card title="Listagem">
        {categoriasQ.isLoading ? (
          <div className="text-sm text-slate-600">Carregando...</div>
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
              { key: "finalidade", header: "Finalidade", cell: (r: Categoria) => finalidadeLabel(r.finalidade) },
              { key: "id", header: "Id", className: "text-slate-500", cell: (r: Categoria) => r.id },
            ]}
          />
        )}
      </Card>

      <Modal
        open={open}
        title="Criar categoria"
        onClose={onClose}
        onConfirm={onConfirm}
        busy={createM.isPending}
        confirmText="Salvar"
      >
        <div className="grid gap-3">
          <Field label="Descrição" value={descricao} onChange={(e) => setDescricao(e.target.value)} />

          <label className="grid gap-1">
            <span className="text-xs font-medium text-slate-700">Finalidade</span>
            <select
              value={finalidade}
              onChange={(e) => setFinalidade(Number(e.target.value) as Finalidade)}
              className="w-full rounded-md border border-slate-300 px-3 py-2 text-sm focus:border-slate-400 focus:outline-none focus:ring-2 focus:ring-slate-200"
            >
              <option value={Finalidade.Despesa}>Despesa</option>
              <option value={Finalidade.Receita}>Receita</option>
              <option value={Finalidade.Ambas}>Ambas</option>
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