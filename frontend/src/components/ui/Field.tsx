import type { ChangeEventHandler } from "react";

type Props = {
  label: string;
  value: string;
  onChange: ChangeEventHandler<HTMLInputElement>;
  placeholder?: string;
  inputMode?: "text" | "numeric" | "decimal" | "email" | "search" | "tel" | "url";
};

export default function Field({ label, value, onChange, placeholder, inputMode }: Props) {
  return (
    <label className="grid gap-1">
      <span className="text-xs font-medium text-slate-700 dark:text-slate-300">{label}</span>
      <input
        value={value}
        onChange={onChange}
        placeholder={placeholder}
        inputMode={inputMode}
        className="w-full rounded-md border border-slate-300 bg-white px-3 py-2 text-sm text-slate-900 outline-none focus:border-slate-400 focus:ring-2 focus:ring-slate-200 dark:border-slate-700 dark:bg-slate-900 dark:text-slate-100 dark:focus:border-slate-500 dark:focus:ring-slate-800"
      />
    </label>
  );
}
