import type { ReactNode } from "react";

type Props = {
  title: string;
  action?: ReactNode;
  subtitle?: ReactNode;
};

export default function PageHeader({ title, action, subtitle }: Props) {
  return (
    <div className="flex flex-wrap items-start justify-between gap-3">
      <div>
        <h1 className="text-xl font-semibold text-slate-900 dark:text-slate-100">{title}</h1>
        {subtitle ? <div className="mt-1 text-sm text-slate-600 dark:text-slate-300">{subtitle}</div> : null}
      </div>
      {action}
    </div>
  );
}
