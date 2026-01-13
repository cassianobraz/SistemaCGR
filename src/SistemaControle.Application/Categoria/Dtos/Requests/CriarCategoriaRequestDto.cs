using MediatR;
using Result.Domain.Models;

namespace SistemaControle.Application.Categoria.Dtos.Requests;

public class CriarCategoriaRequestDto : IRequest<ResultViewModel<bool>>
{
    public string Descricao { get; set; }
    public string Finalidade { get; set; }
}
