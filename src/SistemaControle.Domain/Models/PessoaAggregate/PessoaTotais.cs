namespace SistemaControle.Domain.Models.PessoaAggregate;

public sealed record PessoaTotais(Guid PessoaId, 
                                  string Nome, 
                                  decimal TotalReceitas, 
                                  decimal TotalDespesas)
{
    public decimal Saldo => TotalReceitas - TotalDespesas;
}

public sealed record ConsultaTotaisPorPessoa(IReadOnlyCollection<PessoaTotais> Pessoas,
                                             decimal TotalReceitasGeral,
                                             decimal TotalDespesasGeral)
{
    public decimal SaldoGeral => TotalReceitasGeral - TotalDespesasGeral;
}