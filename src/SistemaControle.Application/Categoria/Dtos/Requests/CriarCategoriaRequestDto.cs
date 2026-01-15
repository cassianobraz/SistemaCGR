using MediatR;
using Result.Domain.Models;
using SistemaControle.Application.Categoria.Dtos.Responses;
using SistemaControle.Domain.Shared.Enums;

namespace SistemaControle.Application.Categoria.Dtos.Requests;

public class CriarCategoriaRequestDto : IRequest<ResultViewModel<CriarCategoriaResponseDto>>
{
    public string? Descricao { get; set; }
    public Finalidade Finalidade { get; set; }
}
