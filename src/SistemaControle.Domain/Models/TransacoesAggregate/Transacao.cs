using SistemaControle.Domain.Models.CategoriaAggregate;
using SistemaControle.Domain.Models.PessoaAggregate;
using SistemaControle.Domain.Shared.Enums;

namespace SistemaControle.Domain.Models.TransacoesAggregate;

public class Transacao
{
    public Guid Id { get; private init; }
    public string Descricao { get; private set; }
    public decimal Valor { get; private set; }
    public TipoTransacao Tipo { get; private set; }
    public Guid CategoriaId { get; private set; }
    public Categoria Categoria { get; set; }
    public Guid PessoaId { get; private set; }
    public Pessoa Pessoa { get; set; }

    protected Transacao() { }

    public Transacao(string descricao, decimal valor, TipoTransacao tipo, Guid categoriaId, Guid pessoaId)
    {
        Id = Guid.NewGuid();
        Descricao = descricao;
        Valor = valor;
        Tipo = tipo;
        CategoriaId = categoriaId;
        PessoaId = pessoaId;
    }

    public static Transacao Create(string descricao, decimal valor, TipoTransacao tipo, Guid categoriaId, Guid pessoaId)
        => new Transacao(descricao, valor, tipo, categoriaId, pessoaId);
}