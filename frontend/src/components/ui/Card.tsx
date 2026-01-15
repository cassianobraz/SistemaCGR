import type { ReactNode } from "react";

type Props = { title?: string; children: ReactNode; right?: ReactNode };

export default function Card({ title, children, right }: Props) {
  return (
    <section className="rounded-xl border border-slate-200 bg-white shadow-sm">
      {(title || right) && (
        <header className="flex items-center justify-between gap-3 border-b border-slate-200 px-4 py-3">
          <h2 className="text-sm font-semibold text-slate-900">{title}</h2>
          {right}
        </header>
      )}
      <div className="p-4">{children}</div>
    </section>
  );
}
