using MediatR;
using Result.Domain.Models;
using SistemaControle.Application.Transacoes.Dtos.Requests;
using SistemaControle.Application.Transacoes.Dtos.Responses;

namespace SistemaControle.Application.Transacoes.Handlers;

public class ObterTransacoesHandler : IRequestHandler<ObterTransacoesRequest, ResultViewModel<TransacaoResponseDto>>
{
    public Task<ResultViewModel<TransacaoResponseDto>> Handle(ObterTransacoesRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
