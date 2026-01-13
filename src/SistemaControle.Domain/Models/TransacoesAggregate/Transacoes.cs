namespace SistemaControle.Domain.Models.TransacoesAggregate;

public class Transacoes
{
    public Guid Id { get; private set; }
    public string Descricao { get; private set; }
    public decimal Valor { get; private set; }
    public string Tipo { get; private set; }
    public Guid CategoriaId { get; private set; }
    public Guid PessoaId { get; private set; }

    protected Transacoes() { }

    public Transacoes(string descricao, decimal valor, string tipo, Guid categoriaId, Guid pessoaId)
    {
        Id = Guid.NewGuid();
        Descricao = descricao;
        Valor = valor;
        Tipo = tipo;
        CategoriaId = categoriaId;
        PessoaId = pessoaId;
    }

    public Transacoes Create(string descricao, decimal valor, string tipo, Guid categoriaId, Guid pessoaId)
        => new Transacoes(descricao, valor, tipo, categoriaId, pessoaId);
}
