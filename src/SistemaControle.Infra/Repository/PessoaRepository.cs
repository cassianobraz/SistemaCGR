using Microsoft.EntityFrameworkCore;
using SistemaControle.Domain.Models.PessoaAggregate;
using SistemaControle.Infra.EF.DbContext;

namespace SistemaControle.Infra.Repository;

public class PessoaRepository : IPessoaRepository
{
    private readonly SistemaControleContext _context;

    public PessoaRepository(SistemaControleContext context)
        => _context = context;

    public Task CriarAsync(Pessoa pessoa, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Pessoa>> ListarTodosAsync(CancellationToken ct)
       => await _context.Set<Pessoa>().AsNoTracking().ToListAsync(ct);
}