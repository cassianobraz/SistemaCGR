import { type ButtonHTMLAttributes, forwardRef } from "react";

type Variant = "primary" | "secondary" | "danger" | "ghost";

type Props = ButtonHTMLAttributes<HTMLButtonElement> & {
  variant?: Variant;
  full?: boolean;
};

const styles: Record<Variant, string> = {
  primary: `
    bg-slate-900 text-white hover:bg-slate-800
    dark:bg-slate-100 dark:text-slate-900 dark:hover:bg-white
    dark:shadow-[0_0_0_1px_rgba(255,255,255,0.18)]
  `,
  secondary: `
    bg-slate-100 text-slate-900 hover:bg-slate-200
    dark:bg-slate-800 dark:text-slate-100 dark:hover:bg-slate-700
    dark:border dark:border-slate-700
  `,
  danger: `
    bg-red-600 text-white hover:bg-red-500
    dark:bg-red-500 dark:text-white dark:hover:bg-red-400
    dark:shadow-[0_0_0_1px_rgba(239,68,68,0.45)]
  `,
  ghost: `
    bg-transparent text-slate-700 hover:bg-slate-100
    dark:text-slate-300 dark:hover:bg-slate-800
  `,
};

const Button = forwardRef<HTMLButtonElement, Props>(function Button(
  { variant = "primary", className = "", full, ...props },
  ref
) {
  return (
    <button
      ref={ref}
      className={[
        "inline-flex items-center justify-center gap-2 rounded-md px-3 py-2 text-sm font-medium transition",
        "focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-slate-400 focus-visible:ring-offset-2",
        "dark:focus-visible:ring-slate-200 dark:focus-visible:ring-offset-slate-950",
        "disabled:opacity-50 disabled:cursor-not-allowed",
        styles[ variant ],
        full ? "w-full" : "",
        className,
      ].join(" ")}
      {...props}
    />
  );
});

export default Button;
