import { ReactNode, useEffect, useId, useRef } from "react";
import Button from "./Button";

type Props = {
  open: boolean;
  title: string;
  children: ReactNode;
  onClose: () => void;
  onConfirm?: () => void;
  confirmText?: string;
  cancelText?: string;
  busy?: boolean;
};

export default function Modal({
  open,
  title,
  children,
  onClose,
  onConfirm,
  confirmText = "Salvar",
  cancelText = "Cancelar",
  busy,
}: Props) {
  const titleId = useId();
  const panelRef = useRef<HTMLDivElement | null>(null);

  useEffect(() => {
    if (!open) return;

    const onKeyDown = (e: KeyboardEvent) => {
      if (e.key === "Escape") onClose();
      if (e.key === "Tab") {
        const root = panelRef.current;
        if (!root) return;

        const focusables = Array.from(
          root.querySelectorAll<HTMLElement>(
            'button,[href],input,select,textarea,[tabindex]:not([tabindex="-1"])'
          )
        ).filter((el) => !el.hasAttribute("disabled"));

        if (focusables.length === 0) return;

        const first = focusables[ 0 ];
        const last = focusables[ focusables.length - 1 ];

        if (!e.shiftKey && document.activeElement === last) {
          e.preventDefault();
          first.focus();
        } else if (e.shiftKey && document.activeElement === first) {
          e.preventDefault();
          last.focus();
        }
      }
    };

    window.addEventListener("keydown", onKeyDown);
    return () => window.removeEventListener("keydown", onKeyDown);
  }, [ open, onClose ]);

  useEffect(() => {
    if (!open) return;
    const t = window.setTimeout(() => {
      const root = panelRef.current;
      const first = root?.querySelector<HTMLElement>("input, button, select, textarea");
      first?.focus();
    }, 0);
    return () => window.clearTimeout(t);
  }, [ open ]);

  if (!open) return null;

  return (
    <div
      role="dialog"
      aria-modal="true"
      aria-labelledby={titleId}
      className="fixed inset-0 z-50 grid place-items-center bg-black/40 p-4"
      onMouseDown={(e) => {
        if (e.target === e.currentTarget) onClose();
      }}
    >
      <div
        ref={panelRef}
        className="w-full max-w-lg rounded-xl border border-slate-200 bg-white shadow-xl"
      >
        <div className="flex items-center justify-between gap-3 border-b border-slate-200 px-4 py-3">
          <h3 id={titleId} className="text-sm font-semibold text-slate-900">
            {title}
          </h3>
          <Button variant="ghost" onClick={onClose} aria-label="Fechar modal">
            ✕
          </Button>
        </div>

        <div className="px-4 py-3">{children}</div>

        <div className="flex items-center justify-end gap-2 border-t border-slate-200 px-4 py-3">
          <Button variant="secondary" onClick={onClose} disabled={busy}>
            {cancelText}
          </Button>
          {onConfirm && (
            <Button onClick={onConfirm} disabled={busy}>
              {busy ? "Salvando..." : confirmText}
            </Button>
          )}
        </div>
      </div>
    </div>
  );
}
