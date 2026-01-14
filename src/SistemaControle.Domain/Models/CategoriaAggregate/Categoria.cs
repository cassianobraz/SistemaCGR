using SistemaControle.Domain.Shared.Enums;

namespace SistemaControle.Domain.Models.CategoriaAggregate;

public class Categoria
{
    public Guid Id { get; private init; }
    public string Descricao { get; private set; }
    public Finalidade Finalidade { get; private set; }

    protected Categoria() { }

    public Categoria(string descricao, Finalidade finalidade)
    {
        Id = Guid.NewGuid();
        Descricao = descricao;
        Finalidade = finalidade;
    }

    public static Categoria Create(string descricao, Finalidade finalidade)
        => new Categoria(descricao, finalidade);
}