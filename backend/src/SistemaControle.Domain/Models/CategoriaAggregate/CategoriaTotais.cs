namespace SistemaControle.Domain.Models.CategoriaAggregate;

public sealed record CategoriaTotais(Guid CategoriaId, string Descricao, decimal TotalReceitas, decimal TotalDespesas)
{
    public decimal Saldo => TotalReceitas - TotalDespesas;
}

public sealed record ConsultaTotaisPorCategoria(IReadOnlyCollection<CategoriaTotais> Categorias,
                                                decimal TotalReceitasGeral,
                                                decimal TotalDespesasGeral)
{
    public decimal SaldoGeral => TotalReceitasGeral - TotalDespesasGeral;
}