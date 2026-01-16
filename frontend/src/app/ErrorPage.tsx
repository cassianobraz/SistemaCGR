import { isRouteErrorResponse, useRouteError, Link } from "react-router-dom";

type ErrorLike = {
  message?: unknown;
  status?: unknown;
  statusText?: unknown;
};

function getErrorMessage(err: unknown) {
  if (isRouteErrorResponse(err)) {
    return `${err.status} ${err.statusText}`;
  }

  if (err && typeof err === "object") {
    const e = err as ErrorLike;
    if (typeof e.message === "string") return e.message;
    if (typeof e.status === "number") return `${e.status}`;
    if (typeof e.statusText === "string") return e.statusText;
  }

  return "Erro inesperado";
}

export default function ErrorPage() {
  const error = useRouteError();
  const message = getErrorMessage(error);

  return (
    <div className="min-h-dvh bg-slate-50 p-6">
      <div className="mx-auto max-w-xl rounded-2xl border border-slate-200 bg-white p-6 shadow-sm">
        <div className="text-sm font-semibold text-slate-900">Ops… deu ruim</div>
        <div className="mt-2 text-sm text-slate-600">{message}</div>

        <div className="mt-5 flex gap-2">
          <Link
            to="/categorias"
            className="inline-flex items-center justify-center rounded-lg bg-slate-900 px-3 py-2 text-sm font-medium text-white hover:bg-slate-800 focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-slate-300"
          >
            Ir pra Categorias
          </Link>

          <button
            type="button"
            onClick={() => window.location.reload()}
            className="inline-flex items-center justify-center rounded-lg border border-slate-200 bg-white px-3 py-2 text-sm font-medium text-slate-900 hover:bg-slate-50 focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-slate-300"
          >
            Recarregar
          </button>
        </div>

        <details className="mt-5 rounded-xl border border-slate-200 bg-slate-50 p-3">
          <summary className="cursor-pointer text-xs font-semibold text-slate-700">Detalhes</summary>
          <pre className="mt-2 overflow-auto text-xs text-slate-700">
            {JSON.stringify(error, null, 2)}
          </pre>
        </details>
      </div>
    </div>
  );
}
