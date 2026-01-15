using MediatR;
using Result.Domain.Models;

namespace SistemaControle.Application.Categoria.Dtos.Requests;

public sealed record ObterTotaisPorCategoriaRequestDto() : IRequest<ResultViewModel<TotaisPorCategoriaResponseDto>>;
