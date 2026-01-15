namespace SistemaControle.Domain.Models.PessoaAggregate;

public interface IPessoaRepository
{
    Task<IEnumerable<Pessoa>> ListarTodosAsync(CancellationToken ct);
    Task CriarAsync(Pessoa pessoa, CancellationToken ct);
    Task ExcluirAsync(Pessoa pessoa, CancellationToken ct);
    Task<Pessoa?> ObterPorIdAsync(Guid id, CancellationToken ct);

    Task<ConsultaTotaisPorPessoa> ConsultarTotaisPorPessoaAsync(CancellationToken ct);
}