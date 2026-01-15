// src/features/pessoa/__tests__/PessoaPage.test.tsx
import { screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { renderWithProviders } from "../../../test/utils/render";
import PessoaPage from "../PessoaPage";
import { describe, expect, it } from 'vitest';

describe("PessoaPage", () => {
  it("renderiza listagem e totais", async () => {
    renderWithProviders(<PessoaPage />);

    expect(await screen.findByText("Totais por pessoa")).toBeInTheDocument();
    expect(await screen.findByText("Maria")).toBeInTheDocument();
    expect(screen.getByText("João")).toBeInTheDocument();
  });

  it("cria uma pessoa via modal", async () => {
    const user = userEvent.setup();
    renderWithProviders(<PessoaPage />);

    await screen.findByText("Listagem");

    await user.click(screen.getByRole("button", { name: /nova pessoa/i }));

    const dialog = await screen.findByRole("dialog");
    await user.type(screen.getByLabelText(/nome/i), "Ana");
    await user.type(screen.getByLabelText(/idade/i), "27");
    await user.click(screen.getByRole("button", { name: /salvar/i }));

    expect(await screen.findByText("Ana")).toBeInTheDocument();
  });

  it("exclui pessoa (somente fluxo happy path)", async () => {
    const user = userEvent.setup();
    const confirmSpy = vi.spyOn(window, "confirm").mockReturnValue(true);

    renderWithProviders(<PessoaPage />);
    await screen.findByText("Maria");

    const mariaRow = screen.getByText("Maria").closest("tr")!;
    await user.click(mariaRow.querySelector("button")!);

    expect(await screen.findByText("João")).toBeInTheDocument();
    confirmSpy.mockRestore();
  });
});
