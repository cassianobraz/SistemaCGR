namespace SistemaControle.Domain.Models.PessoaAggregate;

public class Pessoa
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public int Idade { get; private set; }

    protected Pessoa() { }
    public Pessoa(string nome, int idade)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Idade = idade;
    }

    public Pessoa Create(string nome, int idade)
        => new Pessoa(nome, idade);
}