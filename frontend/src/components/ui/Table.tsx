import { ReactNode } from "react";

type Col<T> = {
  key: string;
  header: string;
  cell: (row: T) => ReactNode;
  className?: string;
};

type Props<T> = {
  columns: Array<Col<T>>;
  rows: unknown;
  empty: ReactNode;
};

function toArray<T>(value: unknown): T[] {
  if (Array.isArray(value)) return value as T[];
  return [];
}

export default function Table<T>({ columns, rows, empty }: Props<T>) {
  const safeRows = toArray<T>(rows);

  return (
    <div className="overflow-x-auto rounded-xl border border-slate-200 bg-white shadow-sm">
      <table className="min-w-full text-left text-sm">
        <thead className="bg-slate-50 text-xs font-semibold uppercase tracking-wide text-slate-600">
          <tr>
            {columns.map((c) => (
              <th key={c.key} className={[ "px-4 py-3", c.className ?? "" ].join(" ")}>
                {c.header}
              </th>
            ))}
          </tr>
        </thead>

        <tbody className="divide-y divide-slate-200">
          {safeRows.length === 0 ? (
            <tr>
              <td className="px-4 py-6 text-center text-slate-500" colSpan={columns.length}>
                {empty}
              </td>
            </tr>
          ) : (
            safeRows.map((r, idx) => (
              <tr key={idx} className="hover:bg-slate-50">
                {columns.map((c) => (
                  <td key={c.key} className={[ "px-4 py-3", c.className ?? "" ].join(" ")}>
                    {c.cell(r)}
                  </td>
                ))}
              </tr>
            ))
          )}
        </tbody>
      </table>
    </div>
  );
}