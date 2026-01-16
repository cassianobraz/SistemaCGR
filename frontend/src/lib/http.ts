export type ApiErrorItem = { code: string; message: string };

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

async function readBody(res: Response): Promise<unknown> {
  const ct = res.headers.get("content-type") ?? "";
  const text = await res.text();

  if (!text) return null;

  if (ct.includes("application/json")) {
    try {
      return JSON.parse(text) as unknown;
    } catch {
      return text;
    }
  }

  try {
    return JSON.parse(text) as unknown;
  } catch {
    return text;
  }
}

function extractMessage(details: unknown): string {
  if (!details) return "Erro";

  if (typeof details === "string") return details;

  if (Array.isArray(details)) {
    const first = details[ 0 ] as unknown;
    if (first && typeof first === "object") {
      const msg = (first as { message?: unknown }).message;
      const code = (first as { code?: unknown }).code;
      if (typeof msg === "string" && typeof code === "string") return `${code}: ${msg}`;
      if (typeof msg === "string") return msg;
    }
    return "Erro";
  }

  if (typeof details === "object") {
    const msg = (details as { message?: unknown }).message;
    if (typeof msg === "string") return msg;
  }

  return "Erro";
}

export async function apiFetch<T>(path: string, init?: RequestInit & { signal?: AbortSignal }): Promise<T> {
  if (!BASE_URL) throw new Error("VITE_API_BASE_URL não configurado");

  const res = await fetch(joinUrl(BASE_URL, path), {
    headers: { "Content-Type": "application/json", ...(init?.headers ?? {}) },
    ...init,
  });

  console.log(res);

  const body = await readBody(res);

  if (!res.ok) {
    const msg = extractMessage(body);
    throw new ApiError(msg, res.status, body);
  }

  return body as T;
}
