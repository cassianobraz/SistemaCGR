using MediatR;
using Result.Domain.Models;
using SistemaControle.Application.Categoria.Dtos.Requests;
using SistemaControle.Application.Categoria.Dtos.Responses;
using SistemaControle.Domain.Services.Interfaces;

namespace SistemaControle.Application.Categoria.Handlers;

public class CriarCategoriaHandler : IRequestHandler<CriarCategoriaRequestDto, ResultViewModel<CriarCategoriaResponseDto>>
{
    private readonly ICategoriaService _categoriaService;
    public CriarCategoriaHandler(ICategoriaService categoriaService)
        => _categoriaService = categoriaService;

    public async Task<ResultViewModel<CriarCategoriaResponseDto>> Handle(CriarCategoriaRequestDto request, CancellationToken ct)
    {
        var result = await _categoriaService.CriarAsync(request.Descricao, request.Finalidade, ct);

        if (result.IsFailure)
            return ResultViewModel<CriarCategoriaResponseDto>.Failure(result.Errors, result.ErrorType!.Value);

        return ResultViewModel<CriarCategoriaResponseDto>.Success(new CriarCategoriaResponseDto(result.Value));
    }
}