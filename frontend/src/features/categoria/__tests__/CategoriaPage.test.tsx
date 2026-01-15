import { screen, within } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { renderWithProviders } from "../../../test/utils/render";
import CategoriaPage from "../CategoriaPage";
import { describe, expect, it } from 'vitest';

describe("CategoriaPage", () => {
  it("renderiza listagem e totais", async () => {
    renderWithProviders(<CategoriaPage />);

    expect(await screen.findByText("Totais por categoria")).toBeInTheDocument();
    expect(await screen.findByText("Salário")).toBeInTheDocument();
    expect(screen.getByText("Mercado")).toBeInTheDocument();

    const totalsCard = screen.getByText("Totais por categoria").closest("section")!;
    expect(within(totalsCard).getByText(/Receitas/i)).toBeInTheDocument();
    expect(within(totalsCard).getByText(/Despesas/i)).toBeInTheDocument();
    expect(within(totalsCard).getByText(/Saldo/i)).toBeInTheDocument();
  });

  it("cria uma categoria via modal", async () => {
    const user = userEvent.setup();
    renderWithProviders(<CategoriaPage />);

    await screen.findByText("Listagem");

    await user.click(screen.getByRole("button", { name: /nova categoria/i }));

    const dialog = await screen.findByRole("dialog");
    await user.type(within(dialog).getByLabelText(/descrição/i), "Transporte");

    await user.selectOptions(within(dialog).getByLabelText(/finalidade/i), "1");
    await user.click(within(dialog).getByRole("button", { name: /salvar/i }));

    expect(await screen.findByText("Transporte")).toBeInTheDocument();
  });
});
