import { useMemo, useState } from "react";
import Modal from "../../components/ui/Modal";
import Field from "../../components/ui/Field";
import { ApiError } from "../../lib/http";
import { useCategorias } from "../categoria/hooks";
import type { Categoria, Finalidade as FinalidadeCategoria } from "../categoria/types";
import { usePessoas } from "../pessoa/hooks";
import type { Pessoa } from "../pessoa/types";
import { useCreateTransacao } from "./hooks";
import { TipoTransacao, type TipoTransacao as TipoTransacaoType } from "./types";

type Props = {
  open: boolean;
  onClose: () => void;
};

function capitalizeFirst(text: string): string {
  const t = text.trim();
  if (!t) return "";
  return t.charAt(0).toUpperCase() + t.slice(1);
}

function parseTipoTransacao(v: string): TipoTransacaoType | "" {
  const n = Number(v);
  if (n === TipoTransacao.Despesa || n === TipoTransacao.Receita) return n;
  return "";
}

function isCategoriaCompativel(c: Categoria, tipo: TipoTransacaoType): boolean {
  if (tipo === TipoTransacao.Despesa) {
    return c.finalidade === (1 as FinalidadeCategoria) || c.finalidade === (3 as FinalidadeCategoria);
  }
  return c.finalidade === (2 as FinalidadeCategoria) || c.finalidade === (3 as FinalidadeCategoria);
}

export default function TransacaoModal({ open, onClose }: Props) {
  const pessoasQ = usePessoas();
  const categoriasQ = useCategorias();
  const createM = useCreateTransacao();

  const [ descricao, setDescricao ] = useState("");
  const [ valor, setValor ] = useState("");
  const [ pessoaId, setPessoaId ] = useState("");
  const [ tipo, setTipo ] = useState<TipoTransacaoType | "">("");
  const [ categoriaId, setCategoriaId ] = useState("");

  const reset = () => {
    setDescricao("");
    setValor("");
    setPessoaId("");
    setTipo("");
    setCategoriaId("");
  };

  const pessoas = useMemo<Pessoa[]>(() => (Array.isArray(pessoasQ.data) ? pessoasQ.data : []), [ pessoasQ.data ]);

  const categorias = useMemo<Categoria[]>(() => (Array.isArray(categoriasQ.data) ? categoriasQ.data : []), [ categoriasQ.data ]);

  const pessoaSelecionada = useMemo<Pessoa | null>(() => {
    if (!pessoaId) return null;
    return pessoas.find((p) => p.id === pessoaId) ?? null;
  }, [ pessoas, pessoaId ]);

  const isMenor = Boolean(pessoaSelecionada && pessoaSelecionada.idade < 18);

  const tipoEfetivo: TipoTransacaoType | "" = isMenor ? TipoTransacao.Despesa : tipo;

  const canPickTipo = Boolean(pessoaId) && !isMenor;
  const canPickCategoria = Boolean(pessoaId) && Boolean(tipoEfetivo);

  const categoriasFiltradas = useMemo<Categoria[]>(() => {
    if (!tipoEfetivo) return [];
    return categorias.filter((c) => isCategoriaCompativel(c, tipoEfetivo));
  }, [ categorias, tipoEfetivo ]);

  const handleClose = () => {
    reset();
    onClose();
  };

  const handleChangePessoa = (id: string) => {
    setPessoaId(id);
    setCategoriaId("");

    const pessoa = pessoas.find((p) => p.id === id);
    if (pessoa && pessoa.idade < 18) {
      setTipo(TipoTransacao.Despesa);
    } else {
      setTipo("");
    }
  };

  const handleChangeTipo = (next: TipoTransacaoType | "") => {
    setTipo(next);
    setCategoriaId("");
  };

  const errorText = (err: unknown) => (err instanceof ApiError ? err.message : "Erro inesperado");

  const onConfirm = async () => {
    const d = capitalizeFirst(descricao);
    const v = Number(String(valor).replace(",", "."));

    if (!d) return;
    if (!pessoaId) return;
    if (!tipoEfetivo) return;
    if (!categoriaId) return;
    if (!Number.isFinite(v) || v <= 0) return;

    await createM.mutateAsync({
      descricao: d,
      valor: v,
      tipo: tipoEfetivo,
      pessoaId,
      categoriaId,
    });

    handleClose();
  };

  return (
    <Modal open={open} title="Criar transação" onClose={handleClose} onConfirm={onConfirm} busy={createM.isPending}>
      <div className="grid gap-3">
        <Field
          label="Descrição"
          value={descricao}
          onChange={(e) => setDescricao(e.target.value)}
          placeholder="Ex: Corte no zero"
        />

        <Field
          label="Valor"
          value={valor}
          onChange={(e) => setValor(e.target.value)}
          placeholder="Ex: 20"
          inputMode="decimal"
        />

        <label className="grid gap-1">
          <span className="text-xs font-medium text-slate-700 dark:text-slate-300">Pessoa</span>
          <select
            value={pessoaId}
            onChange={(e) => handleChangePessoa(e.target.value)}
            className="w-full rounded-md border px-3 py-2 text-sm outline-none transition border-slate-300 bg-white text-slate-900 focus:border-slate-400 focus:ring-2 focus:ring-slate-200 dark:border-slate-700 dark:bg-slate-900 dark:text-slate-100 dark:focus:border-slate-500 dark:focus:ring-slate-800"
          >
            <option value="" disabled>
              Selecione...
            </option>
            {pessoas.map((p) => (
              <option key={p.id} value={p.id} className="bg-white text-slate-900 dark:bg-slate-900 dark:text-slate-100">
                {p.nome}
              </option>
            ))}
          </select>
        </label>

        <label className="grid gap-1">
          <span className="text-xs font-medium text-slate-700 dark:text-slate-300">Tipo</span>
          <select
            value={tipoEfetivo}
            onChange={(e) => handleChangeTipo(parseTipoTransacao(e.target.value))}
            disabled={!canPickTipo}
            className={[
              "w-full rounded-md border px-3 py-2 text-sm outline-none transition",
              "border-slate-300 bg-white text-slate-900 focus:border-slate-400 focus:ring-2 focus:ring-slate-200",
              "dark:border-slate-700 dark:bg-slate-900 dark:text-slate-100 dark:focus:border-slate-500 dark:focus:ring-slate-800",
              !canPickTipo ? "opacity-60 cursor-not-allowed" : "",
            ].join(" ")}
          >
            <option value="" disabled>
              {!pessoaId ? "Selecione uma pessoa primeiro" : isMenor ? "Menor de idade: apenas despesa" : "Selecione..."}
            </option>

            <option value={TipoTransacao.Despesa} className="bg-white text-slate-900 dark:bg-slate-900 dark:text-slate-100">
              Despesa
            </option>

            {!isMenor && (
              <option value={TipoTransacao.Receita} className="bg-white text-slate-900 dark:bg-slate-900 dark:text-slate-100">
                Receita
              </option>
            )}
          </select>
        </label>

        <label className="grid gap-1">
          <span className="text-xs font-medium text-slate-700 dark:text-slate-300">Categoria</span>
          <select
            value={categoriaId}
            onChange={(e) => setCategoriaId(e.target.value)}
            disabled={!canPickCategoria}
            className={[
              "w-full rounded-md border px-3 py-2 text-sm outline-none transition",
              "border-slate-300 bg-white text-slate-900 focus:border-slate-400 focus:ring-2 focus:ring-slate-200",
              "dark:border-slate-700 dark:bg-slate-900 dark:text-slate-100 dark:focus:border-slate-500 dark:focus:ring-slate-800",
              !canPickCategoria ? "opacity-60 cursor-not-allowed" : "",
            ].join(" ")}
          >
            <option value="" disabled>
              {canPickCategoria ? "Selecione..." : "Selecione pessoa e tipo primeiro"}
            </option>

            {categoriasFiltradas.map((c) => (
              <option key={c.id} value={c.id} className="bg-white text-slate-900 dark:bg-slate-900 dark:text-slate-100">
                {c.descricao}
              </option>
            ))}
          </select>
        </label>

        {isMenor && (
          <div className="text-xs font-medium text-slate-700 dark:text-slate-200">
            Menor de 18: apenas <span className="font-semibold">Despesa</span>.
          </div>
        )}

        {createM.isError && (
          <div role="alert" className="text-sm text-red-600">
            {errorText(createM.error)}
          </div>
        )}
      </div>
    </Modal>
  );
}
