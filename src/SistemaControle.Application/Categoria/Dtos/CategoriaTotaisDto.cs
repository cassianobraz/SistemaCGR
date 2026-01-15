namespace SistemaControle.Application.Categoria.Dtos;

public sealed record CategoriaTotaisDto(Guid Id, string Descricao, decimal TotalReceitas, decimal TotalDespesas, decimal Saldo);

public sealed record TotaisPorCategoriaResponseDto(IReadOnlyCollection<CategoriaTotaisDto> Categorias,
                                                   decimal TotalReceitasGeral,
                                                   decimal TotalDespesasGeral,
                                                   decimal SaldoGeral
);
