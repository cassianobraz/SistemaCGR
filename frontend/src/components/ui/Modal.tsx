import type { ReactNode } from "react";

type Props = {
  open: boolean;
  title: string;
  children: ReactNode;
  onClose: () => void;
  onConfirm: () => void;
  busy?: boolean;
  confirmText?: string;
};

export default function Modal({ open, title, children, onClose, onConfirm, busy, confirmText = "Salvar" }: Props) {
  if (!open) return null;

  return (
    <div className="fixed inset-0 z-50 grid place-items-center bg-black/40 p-4">
      <div className="w-full max-w-lg rounded-2xl border border-slate-200 bg-white shadow-xl dark:border-slate-800 dark:bg-slate-900">
        <div className="flex items-center justify-between border-b border-slate-200 px-5 py-4 dark:border-slate-800">
          <div className="text-sm font-semibold text-slate-900 dark:text-slate-100">{title}</div>
          <button
            onClick={onClose}
            className="rounded-lg px-2 py-1 text-slate-600 hover:bg-slate-100 dark:text-slate-300 dark:hover:bg-slate-800"
            aria-label="Fechar"
          >
            ✕
          </button>
        </div>

        <div className="px-5 py-4 text-slate-900 dark:text-slate-100">{children}</div>

        <div className="flex justify-end gap-2 border-t border-slate-200 px-5 py-4 dark:border-slate-800">
          <button
            onClick={onClose}
            className="rounded-lg border border-slate-200 bg-white px-4 py-2 text-sm font-medium text-slate-900 hover:bg-slate-50 dark:border-slate-700 dark:bg-slate-900 dark:text-slate-100 dark:hover:bg-slate-800"
          >
            Cancelar
          </button>
          <button
            onClick={onConfirm}
            disabled={busy}
            className="rounded-lg bg-slate-900 px-4 py-2 text-sm font-semibold text-white hover:bg-slate-800 disabled:opacity-60 dark:bg-slate-100 dark:text-slate-900 dark:hover:bg-white"
          >
            {busy ? "Salvando..." : confirmText}
          </button>
        </div>
      </div>
    </div>
  );
}
