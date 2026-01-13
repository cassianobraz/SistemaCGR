using MediatR;
using Result.Domain.Models;
using SistemaControle.Application.Categoria.Dtos.Requests;

namespace SistemaControle.Application.Categoria.Handlers;

public class CriarCategoriaHandler : IRequestHandler<CriarCategoriaRequestDto, ResultViewModel<bool>>
{
    public Task<ResultViewModel<bool>> Handle(CriarCategoriaRequestDto request, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
