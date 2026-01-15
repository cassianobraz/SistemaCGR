import { type ReactNode, useState } from "react";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { MemoryRouter } from "react-router-dom";
import { ThemeProvider } from "../../app/theme/ThemeProvider";

function makeClient() {
  return new QueryClient({
    defaultOptions: {
      queries: { retry: false, refetchOnWindowFocus: false },
      mutations: { retry: false },
    },
  });
}

export default function Providers({ children }: { children: ReactNode }) {
  const [ client ] = useState(() => makeClient());

  return (
    <ThemeProvider>
      <QueryClientProvider client={client}>
        <MemoryRouter initialEntries={[ "/" ]}>{children}</MemoryRouter>
      </QueryClientProvider>
    </ThemeProvider>
  );
}
