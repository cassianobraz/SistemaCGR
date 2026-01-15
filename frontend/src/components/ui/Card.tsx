import type { ReactNode } from "react";

type Props = {
  title: string;
  right?: ReactNode;
  children: ReactNode;
};

export default function Card({ title, right, children }: Props) {
  return (
    <section className="w-full rounded-2xl border border-slate-200 bg-white shadow-sm dark:border-slate-800 dark:bg-slate-900">
      <header className="flex items-center justify-between gap-3 border-b border-slate-200 px-5 py-4 dark:border-slate-800">
        <h2 className="text-sm font-semibold text-slate-900 dark:text-slate-100">{title}</h2>
        {right}
      </header>
      <div className="p-5 text-slate-900 dark:text-slate-100">{children}</div>
    </section>
  );
}
