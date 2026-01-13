using MediatR;
using Result.Domain.Models;
using SistemaControle.Application.Categoria.Dtos.Requests;
using SistemaControle.Application.Categoria.Dtos.Responses;
using SistemaControle.Domain.Models.CategoriaAggregate;

namespace SistemaControle.Application.Categoria.Handlers;

public class ObterCatetgoriaHandler : IRequestHandler<ObterCategoriasRequestDto, ResultViewModel<CategoriaResponseDto>>
{
    private readonly ICategoriaRepository _categoriaRepository;

    public async Task<ResultViewModel<CategoriaResponseDto>> Handle(ObterCategoriasRequestDto request, CancellationToken ct)
    {
        var result = await _categoriaRepository.ListarTodosAsync(ct);

        var response = new CategoriaResponseDto
        {
            Result = result.Select(c => new CategoriaDto
            {
                Id = c.Id,
                Descricao = c.Descricao,
                Finalidade = c.Finalidade
            }).ToList()
        };

        return ResultViewModel<CategoriaResponseDto>.Success(response);
    }
}
