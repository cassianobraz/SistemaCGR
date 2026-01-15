using MediatR;
using Result.Domain.Models;
using SistemaControle.Application.Pessoa.Dtos;
using SistemaControle.Application.Pessoa.Dtos.Requests;
using SistemaControle.Application.Pessoa.Dtos.Responses;
using SistemaControle.Domain.Models.PessoaAggregate;

namespace SistemaControle.Application.Pessoa.Handlers;

public class ObterReceitaHandler : IRequestHandler<ObterReceitaRequestDto, ResultViewModel<PessoaReceitaResponseDto>>
{
    private readonly IPessoaRepository _pessoaRepository;
    public ObterReceitaHandler(IPessoaRepository pessoaRepository)
        => _pessoaRepository = pessoaRepository;

    public async Task<ResultViewModel<PessoaReceitaResponseDto>> Handle(ObterReceitaRequestDto request, CancellationToken ct)
    {
        var domain = await _pessoaRepository.ConsultarTotaisPorPessoaAsync(ct);

        var pessoas = domain.Pessoas
            .Select(p => new PessoaReceitaDto(
                p.PessoaId,
                p.Nome,
                p.TotalReceitas,
                p.TotalDespesas,
                p.Saldo
            ))
            .ToList();

        var response = new PessoaReceitaResponseDto
        {
            Result = pessoas,
            TotalReceitasGeral = domain.TotalReceitasGeral,
            TotalDespesasGeral = domain.TotalDespesasGeral,
            SaldoGeral = domain.SaldoGeral
        };

        return ResultViewModel<PessoaReceitaResponseDto>.Success(response);
    }
}