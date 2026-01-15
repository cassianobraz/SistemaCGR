export class ApiError extends Error {
  readonly status: number;
  readonly details: unknown;

  constructor(message: string, status: number, details: unknown) {
    super(message);
    this.name = "ApiError";
    this.status = status;
    this.details = details;
  }
}

const BASE_URL = import.meta.env.VITE_API_BASE_URL as string | undefined;

function joinUrl(base: string, path: string) {
  const b = base.replace(/\/+$/, "");
  const p = path.startsWith("/") ? path : `/${path}`;
  return `${b}${p}`;
}

async function safeJson(res: Response): Promise<unknown> {
  const text = await res.text();
  if (!text) return null;
  try {
    return JSON.parse(text) as unknown;
  } catch {
    return text;
  }
}

export async function apiFetch<T>(
  path: string,
  init?: RequestInit & { signal?: AbortSignal }
): Promise<T> {
  if (!BASE_URL) throw new Error("VITE_API_BASE_URL não configurado");

  const res = await fetch(joinUrl(BASE_URL, path), {
    headers: { "Content-Type": "application/json", ...(init?.headers ?? {}) },
    ...init,
  });

  if (!res.ok) {
    const details = await safeJson(res);
    const msg =
      typeof details === "object" && details && "message" in details
        ? String((details as { message?: unknown }).message ?? "Erro")
        : `Erro HTTP ${res.status}`;
    throw new ApiError(msg, res.status, details);
  }

  const data = (await safeJson(res)) as T;
  return data;
}
