using MediatR;
using Result.Domain.Models;
using SistemaControle.Application.Categoria.Dtos.Responses;

namespace SistemaControle.Application.Categoria.Dtos.Requests;

public class ObterCategoriasRequestDto : IRequest<ResultViewModel<CategoriaResponseDto>>
{
}
