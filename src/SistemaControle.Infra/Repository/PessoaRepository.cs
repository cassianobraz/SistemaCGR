using Microsoft.EntityFrameworkCore;
using SistemaControle.Domain.Models.PessoaAggregate;
using SistemaControle.Infra.EF.DbContext;

namespace SistemaControle.Infra.Repository;

public class PessoaRepository : IPessoaRepository
{
    private readonly SistemaControleContext _context;

    public PessoaRepository(SistemaControleContext context)
        => _context = context;

    public async Task CriarAsync(Pessoa pessoa, CancellationToken ct)
     => await _context.Set<Pessoa>().AddAsync(pessoa, ct);

    public async Task ExcluirAsync(Pessoa pessoa, CancellationToken ct)
        => _context.Set<Pessoa>().Remove(pessoa);

    public async Task<IEnumerable<Pessoa>> ListarTodosAsync(CancellationToken ct)
       => await _context.Set<Pessoa>().AsNoTracking().ToListAsync(ct);

    public Task<Pessoa?> ObterPorIdAsync(Guid id, CancellationToken ct)
        => _context.Set<Pessoa>().AsNoTracking().FirstOrDefaultAsync(p => p.Id.Equals(id), ct);
}