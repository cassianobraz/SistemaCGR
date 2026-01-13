using MediatR;
using Result.Domain.Models;
using SistemaControle.Application.Transacoes.Dtos.Requests;

namespace SistemaControle.Application.Transacoes.Handlers;

public class CriarTransacoesHandler : IRequestHandler<CriarTranscoesRequest, ResultViewModel<bool>>
{
    public Task<ResultViewModel<bool>> Handle(CriarTranscoesRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
