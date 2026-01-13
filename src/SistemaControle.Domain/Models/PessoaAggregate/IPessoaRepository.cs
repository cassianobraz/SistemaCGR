namespace SistemaControle.Domain.Models.PessoaAggregate;

public interface IPessoaRepository
{
    Task<IEnumerable<Pessoa>> ListarTodosAsync(CancellationToken ct);
    Task CriarAsync(Pessoa pessoa, CancellationToken ct);
}
