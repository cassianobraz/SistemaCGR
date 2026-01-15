import type { SelectHTMLAttributes } from "react";

type Option = { value: string; label: string; disabled?: boolean };

type Props = SelectHTMLAttributes<HTMLSelectElement> & {
  label?: string;
  options: Option[];
};

export default function Select({ label, options, className = "", ...props }: Props) {
  return (
    <label className="grid gap-1">
      {label ? (
        <span className="text-xs font-medium text-slate-700 dark:text-slate-300">{label}</span>
      ) : null}

      <select
        {...props}
        className={[
          "w-full rounded-md border px-3 py-2 text-sm outline-none transition",
          "border-slate-300 bg-white text-slate-900",
          "focus:border-slate-400 focus:ring-2 focus:ring-slate-200",
          "dark:border-slate-700 dark:bg-slate-900 dark:text-slate-100",
          "dark:focus:border-slate-500 dark:focus:ring-slate-800",
          className,
        ].join(" ")}
      >
        {options.map((o) => (
          <option
            key={o.value}
            value={o.value}
            disabled={o.disabled}
            className="bg-white text-slate-900 dark:bg-slate-900 dark:text-slate-100"
          >
            {o.label}
          </option>
        ))}
      </select>
    </label>
  );
}
