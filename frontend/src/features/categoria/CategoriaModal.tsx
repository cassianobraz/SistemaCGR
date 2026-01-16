import { useMemo, useState } from "react";
import Modal from "../../components/ui/Modal";
import Field from "../../components/ui/Field";
import Select from "../../components/ui/Select";
import { ApiError } from "../../lib/http";
import { Finalidade, type CategoriaCreate } from "./types";

type Props = {
  open: boolean;
  onClose: () => void;
  onSubmit: (payload: CategoriaCreate) => Promise<void>;
  busy?: boolean;
  error?: unknown;
};

function capitalizeFirst(text: string): string {
  const t = text.trim();
  if (!t) return "";
  return t.charAt(0).toUpperCase() + t.slice(1);
}

export default function CategoriaModal({ open, onClose, onSubmit, busy, error }: Props) {
  const [ descricao, setDescricao ] = useState("");
  const [ finalidade, setFinalidade ] = useState<Finalidade>(Finalidade.Despesa);

  const errorText = useMemo(() => (error instanceof ApiError ? error.message : ""), [ error ]);

  const close = () => {
    onClose();
    setDescricao("");
    setFinalidade(Finalidade.Despesa);
  };

  const confirm = async () => {
    const d = capitalizeFirst(descricao);
    if (!d) return;

    await onSubmit({ descricao: d, finalidade });
    close();
  };

  return (
    <Modal open={open} title="Criar categoria" onClose={close} onConfirm={confirm} busy={busy}>
      <div className="grid gap-3">
        <Field
          label="Descrição"
          value={descricao}
          onChange={(e) => setDescricao(e.target.value)}
          placeholder="Ex: Salário"
        />

        <Select
          label="Finalidade"
          value={String(finalidade)}
          onChange={(e) => setFinalidade(Number(e.target.value) as Finalidade)}
          options={[
            { value: String(Finalidade.Despesa), label: "Despesa" },
            { value: String(Finalidade.Receita), label: "Receita" },
            { value: String(Finalidade.Ambas), label: "Ambas" },
          ]}
        />

        {errorText ? (
          <div role="alert" className="text-sm text-red-600">
            {errorText}
          </div>
        ) : null}
      </div>
    </Modal>
  );
}
