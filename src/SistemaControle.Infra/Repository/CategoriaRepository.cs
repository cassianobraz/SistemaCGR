using Microsoft.EntityFrameworkCore;
using SistemaControle.Domain.Models.CategoriaAggregate;
using SistemaControle.Infra.EF.DbContext;

namespace SistemaControle.Infra.Repository;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly SistemaControleContext _context;

    public CategoriaRepository(SistemaControleContext context)
        => _context = context;

    public async Task CriarAsync(Categoria categoria, CancellationToken ct)
        => await _context.Set<Categoria>().AddAsync(categoria, ct);

    public async Task<IEnumerable<Categoria>> ListarTodosAsync(CancellationToken ct)
        => await _context.Set<Categoria>().AsNoTracking().ToListAsync(ct);
}
