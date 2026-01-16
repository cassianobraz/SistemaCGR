import { createBrowserRouter, Navigate } from "react-router-dom";
import SidebarLayout from "../components/layout/SidebarLayout";
import CategoriaPage from "../features/categoria/CategoriaPage";
import PessoaPage from "../features/pessoa/PessoaPage";
import TransacaoPage from "../features/transacao/TransacaoPage";
import ErrorPage from "./ErrorPage";

export const appRouter = createBrowserRouter([
  {
    element: <SidebarLayout />,
    errorElement: <ErrorPage />,
    children: [
      { path: "/", element: <Navigate to="/categorias" replace /> },
      { path: "/categorias", element: <CategoriaPage /> },
      { path: "/pessoas", element: <PessoaPage /> },
      { path: "/transacoes", element: <TransacaoPage /> },
    ],
  },
]);