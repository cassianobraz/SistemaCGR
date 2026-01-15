import { NavLink } from "react-router-dom";
import { useTheme } from "../../app/theme/useTheme";
import { Sun, Moon } from "lucide-react";

const linkBase =
  "flex items-center gap-2 rounded-lg px-3 py-2 text-sm font-medium transition-colors";

export default function SidebarNav() {
  const { theme, toggleTheme } = useTheme();

  return (
    <aside className="fixed left-0 top-0 z-40 h-dvh w-[280px] border-r border-slate-200 bg-white dark:bg-slate-900 dark:border-slate-800">
      <div className="flex h-full flex-col p-5">
        {/* Header */}
        <div className="mb-6 flex items-center justify-between">
          <div>
            <div className="text-[11px] font-semibold uppercase tracking-wider text-slate-500 dark:text-slate-400">
              Financeiro
            </div>
            <div className="text-xl font-semibold text-slate-900 dark:text-slate-100">
              Painel
            </div>
          </div>

          <button
            onClick={toggleTheme}
            aria-label="Alternar tema"
            className="rounded-lg border border-slate-200 p-2 text-slate-700 hover:bg-slate-100 dark:border-slate-700 dark:text-slate-200 dark:hover:bg-slate-800"
          >
            {theme === "light" ? <Moon size={18} /> : <Sun size={18} />}
          </button>
        </div>

        {/* Navegação */}
        <nav className="grid gap-1">
          <NavLink
            to="/categorias"
            className={({ isActive }) =>
              [
                linkBase,
                isActive
                  ? "bg-slate-900 text-white dark:bg-slate-100 dark:text-slate-900"
                  : "text-slate-700 hover:bg-slate-100 dark:text-slate-300 dark:hover:bg-slate-800",
              ].join(" ")
            }
          >
            Categorias
          </NavLink>

          <NavLink
            to="/pessoas"
            className={({ isActive }) =>
              [
                linkBase,
                isActive
                  ? "bg-slate-900 text-white dark:bg-slate-100 dark:text-slate-900"
                  : "text-slate-700 hover:bg-slate-100 dark:text-slate-300 dark:hover:bg-slate-800",
              ].join(" ")
            }
          >
            Pessoas
          </NavLink>

          <NavLink
            to="/transacoes"
            className={({ isActive }) =>
              [
                linkBase,
                isActive
                  ? "bg-slate-900 text-white dark:bg-slate-100 dark:text-slate-900"
                  : "text-slate-700 hover:bg-slate-100 dark:text-slate-300 dark:hover:bg-slate-800",
              ].join(" ")
            }
          >
            Transações
          </NavLink>
        </nav>
      </div>
    </aside>
  );
}
