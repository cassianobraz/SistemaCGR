import React from "react";
import ReactDOM from "react-dom/client";
import { RouterProvider } from "react-router-dom";
import { appRouter } from "./app/router";
import { ThemeProvider } from "./app/theme/ThemeProvider";
import { QueryProvider } from "./app/query/QueryProvider";
import "./index.css";

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <ThemeProvider>
      <QueryProvider>
        <RouterProvider router={appRouter} />
      </QueryProvider>
    </ThemeProvider>
  </React.StrictMode>
);
