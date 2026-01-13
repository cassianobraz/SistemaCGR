namespace SistemaControle.Domain.Models.TransacoesAggregate;

public interface ITransacoesRepository
{
    Task<IEnumerable<Transacoes>> ListarTodosAsync(CancellationToken ct);
}
