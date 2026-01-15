namespace SistemaControle.Application.Pessoa.Dtos;

public sealed record PessoaReceitaDto(
    Guid Id,
    string Nome,
    decimal TotalReceitas,
    decimal TotalDespesas,
    decimal Saldo
);

public sealed record ConsultaTotaisPorPessoaDto(
    IReadOnlyCollection<PessoaReceitaDto> Pessoas,
    decimal TotalReceitasGeral,
    decimal TotalDespesasGeral,
    decimal SaldoGeral
);