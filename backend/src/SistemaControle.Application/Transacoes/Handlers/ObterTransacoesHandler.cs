using MediatR;
using Result.Domain.Models;
using SistemaControle.Application.Transacoes.Dtos;
using SistemaControle.Application.Transacoes.Dtos.Requests;
using SistemaControle.Application.Transacoes.Dtos.Responses;
using SistemaControle.Domain.Models.TransacoesAggregate;

namespace SistemaControle.Application.Transacoes.Handlers;

public class ObterTransacoesHandler : IRequestHandler<ObterTransacoesRequest, ResultViewModel<TransacaoResponseDto>>
{
    private readonly ITransacoesRepository _transacoesRepository;
    public ObterTransacoesHandler(ITransacoesRepository transacoesRepository)
        => _transacoesRepository = transacoesRepository;

    public async Task<ResultViewModel<TransacaoResponseDto>> Handle(ObterTransacoesRequest request, CancellationToken ct)
    {
       var transacao = await _transacoesRepository.ListarTodosAsync(ct);

        var response = new TransacaoResponseDto
        {
            Result = transacao.Select(c => new TransacaoDto
            {
                Id = c.Id,
                Descricao = c.Descricao,
                Valor = c.Valor,
                Tipo = c.Tipo,
                CategoriaId = c.CategoriaId,
                PessoaId = c.PessoaId
            }).ToList()
        };

        return ResultViewModel<TransacaoResponseDto>.Success(response);
    }
}
