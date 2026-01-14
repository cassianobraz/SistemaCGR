using SistemaControle.Domain.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace SistemaControle.Application.Categoria;

public class CategoriaDto
{
    public Guid Id { get; set; }
    public string Descricao { get; set; }
    [EnumDataType(typeof(Finalidade), ErrorMessage = "Finalidade inválida. Use: Despesa (1), Receita (2) ou Ambas (3).")]
    public Finalidade Finalidade { get; set; }
}
