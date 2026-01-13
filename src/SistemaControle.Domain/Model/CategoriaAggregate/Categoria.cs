namespace SistemaControle.Domain.Model.CategoriaAggregate;

public class Categoria
{
    public Guid Id { get; private set; }
    public string Descricao { get; private set; }
    public string Finalidade { get; private set; }

    protected Categoria() { }
    public Categoria(string descricao, string finalidade)
    {
        Id = Guid.NewGuid();
        Descricao = descricao;
        Finalidade = finalidade;
    }

    public Categoria Create(string descricao, string finalidade)
        => new Categoria(descricao, finalidade);
}