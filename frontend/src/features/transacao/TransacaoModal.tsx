import { useMemo, useState } from "react";
import Modal from "../../components/ui/Modal";
import Field from "../../components/ui/Field";
import Select from "../../components/ui/Select";
import { ApiError } from "../../lib/http";
import type { Categoria } from "../categoria/types";
import type { Pessoa } from "../pessoa/types";
import { TipoTransacao, type TransacaoCreate } from "./types";

type Props = {
  open: boolean;
  onClose: () => void;
  onSubmit: (payload: TransacaoCreate) => Promise<void>;
  busy?: boolean;
  error?: unknown;
  pessoas: Pessoa[];
  categorias: Categoria[];
};

export default function TransacaoModal({ open, onClose, onSubmit, busy, error, pessoas, categorias }: Props) {
  const [ descricao, setDescricao ] = useState("");
  const [ valor, setValor ] = useState("");
  const [ tipo, setTipo ] = useState<TipoTransacao>(TipoTransacao.Despesa);
  const [ pessoaId, setPessoaId ] = useState("");
  const [ categoriaId, setCategoriaId ] = useState("");

  const errorText = useMemo(() => (error instanceof ApiError ? error.message : ""), [ error ]);

  const pessoasOptions = useMemo(
    () => [
      { value: "", label: "Selecione...", disabled: true },
      ...pessoas.map((p) => ({ value: p.id, label: p.nome })),
    ],
    [ pessoas ]
  );

  const categoriasOptions = useMemo(
    () => [
      { value: "", label: "Selecione...", disabled: true },
      ...categorias.map((c) => ({ value: c.id, label: c.descricao })),
    ],
    [ categorias ]
  );

  const close = () => {
    onClose();
    setDescricao("");
    setValor("");
    setTipo(TipoTransacao.Despesa);
    setPessoaId("");
    setCategoriaId("");
  };

  const confirm = async () => {
    const d = descricao.trim();
    const v = Number(valor);

    if (!d) return;
    if (!Number.isFinite(v) || v <= 0) return;
    if (!pessoaId || !categoriaId) return;

    await onSubmit({
      descricao: d,
      valor: v,
      tipo,
      pessoaId,
      categoriaId,
    });

    close();
  };

  return (
    <Modal open={open} title="Criar transação" onClose={close} onConfirm={confirm} busy={busy}>
      <div className="grid gap-3">
        <Field label="Descrição" value={descricao} onChange={(e) => setDescricao(e.target.value)} placeholder="Ex: Salário do mês" />
        <Field label="Valor" value={valor} onChange={(e) => setValor(e.target.value)} inputMode="decimal" placeholder="Ex: 3000" />

        <Select
          label="Tipo"
          value={String(tipo)}
          onChange={(e) => setTipo(Number(e.target.value) as TipoTransacao)}
          options={[
            { value: String(TipoTransacao.Despesa), label: "Despesa" },
            { value: String(TipoTransacao.Receita), label: "Receita" },
          ]}
        />

        <Select label="Pessoa" value={pessoaId} onChange={(e) => setPessoaId(e.target.value)} options={pessoasOptions} />
        <Select label="Categoria" value={categoriaId} onChange={(e) => setCategoriaId(e.target.value)} options={categoriasOptions} />

        {errorText ? <div role="alert" className="text-sm text-red-600">{errorText}</div> : null}
      </div>
    </Modal>
  );
}
