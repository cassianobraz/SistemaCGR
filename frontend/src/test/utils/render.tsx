import { render } from "@testing-library/react";
import type { ReactNode } from "react";
import Providers from "./Providers";

export function renderWithProviders(ui: ReactNode) {
  return render(<Providers>{ui}</Providers>);
}
