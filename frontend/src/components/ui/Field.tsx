import { InputHTMLAttributes, ReactNode } from "react";

type Props = InputHTMLAttributes<HTMLInputElement> & {
  label: string;
  error?: string;
  right?: ReactNode;
};

export default function Field({ label, error, right, className = "", ...props }: Props) {
  return (
    <label className="grid gap-1">
      <span className="text-xs font-medium text-slate-700">{label}</span>
      <div className="relative">
        <input
          className={[
            "w-full rounded-md border px-3 py-2 text-sm",
            "border-slate-300 focus:border-slate-400 focus:outline-none focus:ring-2 focus:ring-slate-200",
            error ? "border-red-400 focus:ring-red-100" : "",
            className,
          ].join(" ")}
          {...props}
        />
        {right && <div className="absolute right-2 top-1/2 -translate-y-1/2">{right}</div>}
      </div>
      {error && <span className="text-xs text-red-600">{error}</span>}
    </label>
  );
}
