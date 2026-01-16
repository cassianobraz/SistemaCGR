import { Outlet } from "react-router-dom";
import SidebarNav from "./SidebarNav";

const SIDEBAR_W = 280;

export default function SidebarLayout() {
  return (
    <div className="min-h-dvh bg-slate-50 text-slate-900 dark:bg-slate-950 dark:text-slate-100">
      <SidebarNav />

      <div style={{ paddingLeft: SIDEBAR_W }} className="min-h-dvh">
        <main className="w-full px-8 py-6">{<Outlet />}</main>
      </div>
    </div>
  );
}
