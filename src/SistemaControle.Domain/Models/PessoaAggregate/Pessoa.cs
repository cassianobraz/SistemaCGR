using SistemaControle.Domain.Models.TransacoesAggregate;

namespace SistemaControle.Domain.Models.PessoaAggregate;

public class Pessoa
{
    public Guid Id { get; private init; }
    public string Nome { get; private set; }
    public int Idade { get; private set; }
    public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();

    protected Pessoa() { }
    public Pessoa(string nome, int idade)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Idade = idade;
    }

    public static Pessoa Create(string nome, int idade)
        => new Pessoa(nome, idade);
}