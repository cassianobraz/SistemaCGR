import { useMemo, useState } from "react";
import Modal from "../../components/ui/Modal";
import Field from "../../components/ui/Field";
import { ApiError } from "../../lib/http";
import type { PessoaCreate } from "./types";

type Props = {
  open: boolean;
  onClose: () => void;
  onSubmit: (payload: PessoaCreate) => Promise<void>;
  busy?: boolean;
  error?: unknown;
};

function capitalizeFirst(text: string): string {
  const t = text.trim();
  if (!t) return "";
  return t.charAt(0).toUpperCase() + t.slice(1);
}

export default function PessoaModal({ open, onClose, onSubmit, busy, error }: Props) {
  const [ nome, setNome ] = useState("");
  const [ idade, setIdade ] = useState("");

  const errorText = useMemo(() => (error instanceof ApiError ? error.message : ""), [ error ]);

  const close = () => {
    onClose();
    setNome("");
    setIdade("");
  };

  const confirm = async () => {
    const n = capitalizeFirst(nome);
    const i = Number(idade);

    if (!n) return;
    if (!Number.isFinite(i) || i < 0) return;

    await onSubmit({ nome: n, idade: i });
    close();
  };

  return (
    <Modal open={open} title="Criar pessoa" onClose={close} onConfirm={confirm} busy={busy}>
      <div className="grid gap-3">
        <Field
          label="Nome"
          value={nome}
          onChange={(e) => setNome(e.target.value)}
          placeholder="Ex: Cassiano"
        />
        <Field
          label="Idade"
          value={idade}
          onChange={(e) => setIdade(e.target.value)}
          inputMode="numeric"
          placeholder="Ex: 30"
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
