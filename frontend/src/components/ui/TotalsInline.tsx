import { currencyBRL } from "../../lib/format";

type Props = {
  receitas: number;
  despesas: number;
  saldo: number;
};

function Chip({ label, value }: { label: string; value: string }) {
  return (
    <div className="flex items-center gap-2 rounded-full border border-slate-200 bg-white px-3 py-1 text-xs font-semibold text-slate-800 shadow-sm dark:border-slate-700 dark:bg-slate-800 dark:text-slate-100">
      <span className="text-slate-600 dark:text-slate-300">{label}</span>
      <span className="tabular-nums">{value}</span>
    </div>
  );
}

export default function TotalsInline({ receitas, despesas, saldo }: Props) {
  return (
    <div className="flex flex-wrap items-center justify-end gap-2">
      <Chip label="Receitas" value={currencyBRL(receitas)} />
      <Chip label="Despesas" value={currencyBRL(despesas)} />
      <Chip label="Saldo" value={currencyBRL(saldo)} />
    </div>
  );
}
