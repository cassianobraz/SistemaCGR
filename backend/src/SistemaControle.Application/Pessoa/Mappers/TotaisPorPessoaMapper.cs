using SistemaControle.Application.Pessoa.Dtos;
using SistemaControle.Domain.Models.PessoaAggregate;

namespace SistemaControle.Application.Pessoa.Mappers;

public static class TotaisPorPessoaMapper
{
    public static ConsultaTotaisPorPessoaDto ToDto(this ConsultaTotaisPorPessoa domain)
        => new(
            Pessoas: domain.Pessoas.Select(p => new PessoaReceitaDto(
                Id: p.PessoaId,
                Nome: p.Nome,
                TotalReceitas: p.TotalReceitas,
                TotalDespesas: p.TotalDespesas,
                Saldo: p.Saldo
            )).ToList(),
            TotalReceitasGeral: domain.TotalReceitasGeral,
            TotalDespesasGeral: domain.TotalDespesasGeral,
            SaldoGeral: domain.SaldoGeral
        );
}
