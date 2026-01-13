using SistemaControle.Domain.Models.PessoaAggregate;

namespace SistemaControle.Infra.Repository;

public class PessoaRepository : IPessoaRepository
{
    public Task<IEnumerable<Pessoa>> ListarTodosAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
