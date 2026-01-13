using MediatR;
using Result.Domain.Models;
using SistemaControle.Application.Categoria.Dtos.Requests;
using SistemaControle.Application.Categoria.Dtos.Responses;

namespace SistemaControle.Application.Categoria.Handlers;

public class ObterCatetgoriaHandler : IRequestHandler<ObterCategoriasRequestDto, ResultViewModel<CategoriaResponseDto>>
{
    public Task<ResultViewModel<CategoriaResponseDto>> Handle(ObterCategoriasRequestDto request, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
