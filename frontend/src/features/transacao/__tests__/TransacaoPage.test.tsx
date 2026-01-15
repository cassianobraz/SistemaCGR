import { screen, within } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { renderWithProviders } from "../../../test/utils/render";
import TransacaoPage from "../TransacaoPage";
import { expect, it } from "vitest";

it("cria transação via modal", async () => {
  const user = userEvent.setup();
  renderWithProviders(<TransacaoPage />);

  await user.click(screen.getByRole("button", { name: /nova transação/i }));

  const title = await screen.findByText("Criar transação");
  const modal = title.closest(".max-w-lg") as HTMLElement;

  await user.type(within(modal).getByLabelText(/descrição/i), "Uber");
  await user.type(within(modal).getByLabelText(/valor/i), "35");
  await user.selectOptions(within(modal).getByLabelText(/tipo/i), "1");

  const pessoaSelect = within(modal).getByLabelText(/^pessoa$/i) as HTMLSelectElement;
  const categoriaSelect = within(modal).getByLabelText(/^categoria$/i) as HTMLSelectElement;

  const mariaOption = await within(pessoaSelect).findByRole("option", { name: "Maria" });
  const mercadoOption = await within(categoriaSelect).findByRole("option", { name: "Mercado" });

  await user.selectOptions(pessoaSelect, (mariaOption as HTMLOptionElement).value);
  await user.selectOptions(categoriaSelect, (mercadoOption as HTMLOptionElement).value);

  await user.click(within(modal).getByRole("button", { name: /salvar/i }));

  expect(await screen.findByText("Uber")).toBeInTheDocument();
});
