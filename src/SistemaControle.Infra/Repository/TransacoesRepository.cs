using SistemaControle.Domain.Models.TransacoesAggregate;

namespace SistemaControle.Infra.Repository;

public class TransacoesRepository : ITransacoesRepository
{
    public Task<IEnumerable<Transacoes>> ListarTodosAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
