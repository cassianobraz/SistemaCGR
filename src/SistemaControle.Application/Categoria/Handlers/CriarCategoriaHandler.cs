using MediatR;
using Result.Domain.Models;
using SistemaControle.Application.Categoria.Dtos.Requests;
using SistemaControle.Domain.Services.Interfaces;

namespace SistemaControle.Application.Categoria.Handlers;

public class CriarCategoriaHandler : IRequestHandler<CriarCategoriaRequestDto, ResultViewModel<bool>>
{
    private readonly ICategoriaService _categoriaService;
    public async Task<ResultViewModel<bool>> Handle(CriarCategoriaRequestDto request, CancellationToken ct)
    {
        var result = await _categoriaService.CriarAsync(request.Descricao, request.Finalidade, ct);

        if (result.IsFailure)
            return ResultViewModel<bool>.Failure(result.Errors, result.ErrorType!.Value);

        return ResultViewModel<bool>.Success(true);
    }
}