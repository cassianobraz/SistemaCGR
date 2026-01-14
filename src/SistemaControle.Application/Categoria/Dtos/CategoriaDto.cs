using SistemaControle.Domain.Shared.Enums;

namespace SistemaControle.Application.Categoria.Dtos;

public class CategoriaDto
{
    public Guid Id { get; set; }
    public string Descricao { get; set; }
    public Finalidade Finalidade { get; set; }
}
