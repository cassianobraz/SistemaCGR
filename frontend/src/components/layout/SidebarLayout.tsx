import { Outlet } from "react-router-dom";
import SidebarNav from "./SidebarNav";

export default function SidebarLayout() {
  return (
    <div className="min-h-dvh bg-slate-50 text-slate-900">
      <div className="mx-auto grid max-w-7xl grid-cols-1 gap-4 p-4 md:grid-cols-[260px_1fr]">
        <aside className="md:sticky md:top-4 md:h-[calc(100dvh-2rem)]">
          <SidebarNav />
        </aside>

        <main className="grid gap-4">
          <Outlet />
        </main>
      </div>
    </div>
  );
}
