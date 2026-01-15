import { NavLink } from "react-router-dom";

const linkBase =
  "flex items-center gap-2 rounded-md px-3 py-2 text-sm font-medium focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-slate-300";

export default function SidebarNav() {
  return (
    <nav aria-label="Navegação principal" className="rounded-xl border border-slate-200 bg-white p-3 shadow-sm">
      <div className="px-2 pb-2">
        <div className="text-xs font-semibold uppercase tracking-wide text-slate-500">Financeiro</div>
        <div className="text-sm font-semibold text-slate-900">Painel</div>
      </div>

      <div className="grid gap-1">
        <NavLink
          to="/categorias"
          className={({ isActive }) =>
            [ linkBase, isActive ? "bg-slate-900 text-white" : "text-slate-700 hover:bg-slate-100" ].join(" ")
          }
        >
          Categorias
        </NavLink>

        <NavLink
          to="/pessoas"
          className={({ isActive }) =>
            [ linkBase, isActive ? "bg-slate-900 text-white" : "text-slate-700 hover:bg-slate-100" ].join(" ")
          }
        >
          Pessoas
        </NavLink>

        <NavLink
          to="/transacoes"
          className={({ isActive }) =>
            [ linkBase, isActive ? "bg-slate-900 text-white" : "text-slate-700 hover:bg-slate-100" ].join(" ")
          }
        >
          Transações
        </NavLink>
      </div>

      <div className="mt-3 border-t border-slate-200 pt-3 text-xs text-slate-500">
        Base URL: <span className="font-medium">{import.meta.env.VITE_API_BASE_URL}</span>
      </div>
    </nav>
  );
}
