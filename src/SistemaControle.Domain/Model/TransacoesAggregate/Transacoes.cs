using SistemaControle.Domain.Model.CategoriaAggregate;

namespace SistemaControle.Domain.Model.TransacoesAggregate;

public class Transacoes
{
    public Guid Id { get; private set; }
    public string Descricao { get; private set; }
    public decimal Valor { get; private set; }
    public string Tipo { get; private set; }
    public Categoria Categoria { get; private set; }
    public Guid PessoaId { get; private set; }

    protected Transacoes() { }

    public Transacoes(string descricao, decimal valor, string tipo, Categoria categoria, Guid pessoaId)
    {
        Id = Guid.NewGuid();
        Descricao = descricao;
        Valor = valor;
        Tipo = tipo;
        Categoria = categoria;
        PessoaId = pessoaId;
    }

    public Transacoes Create(string descricao, decimal valor, string tipo, Categoria categoria, Guid pessoaId)
        => new Transacoes(descricao, valor, tipo, categoria, pessoaId);
}
