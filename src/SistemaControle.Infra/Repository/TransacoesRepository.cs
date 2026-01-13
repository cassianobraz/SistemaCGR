using Microsoft.EntityFrameworkCore;
using SistemaControle.Domain.Models.TransacoesAggregate;
using SistemaControle.Infra.EF.DbContext;

namespace SistemaControle.Infra.Repository;

public class TransacoesRepository : ITransacoesRepository
{
    private readonly SistemaControleContext _context;

    public TransacoesRepository(SistemaControleContext context)
        => _context = context;

    public Task CriarAsync(Transacoes transacoes, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Transacoes>> ListarTodosAsync(CancellationToken ct)
        => await _context.Set<Transacoes>().AsNoTracking().ToListAsync(ct);
}