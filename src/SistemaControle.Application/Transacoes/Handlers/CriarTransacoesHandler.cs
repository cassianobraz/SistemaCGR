using MediatR;
using Result.Domain.Models;
using SistemaControle.Application.Transacoes.Dtos.Requests;
using SistemaControle.Domain.Services.Interfaces;

namespace SistemaControle.Application.Transacoes.Handlers;

public class CriarTransacoesHandler : IRequestHandler<CriarTranscoesRequest, ResultViewModel<bool>>
{
    private readonly ITransacoesService _transacoesService;
    public CriarTransacoesHandler(ITransacoesService transacoesService)
        => _transacoesService = transacoesService;

    public async Task<ResultViewModel<bool>> Handle(CriarTranscoesRequest request, CancellationToken ct)
    {
        var result = await _transacoesService.CriarAsync(request.Descricao, request.Valor, request.Tipo, request.CategoriaId, request.PessoaId, ct);

        if (result.IsFailure)
            return ResultViewModel<bool>.Failure(result.Errors, result.ErrorType!.Value);

        return ResultViewModel<bool>.Success(true);
    }
}
