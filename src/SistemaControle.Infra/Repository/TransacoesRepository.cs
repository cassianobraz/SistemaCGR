using Microsoft.EntityFrameworkCore;
using SistemaControle.Domain.Models.CategoriaAggregate;
using SistemaControle.Domain.Models.PessoaAggregate;
using SistemaControle.Domain.Models.TransacoesAggregate;
using SistemaControle.Infra.EF.DbContext;

namespace SistemaControle.Infra.Repository;

public class TransacoesRepository : ITransacoesRepository
{
    private readonly SistemaControleContext _context;

    public TransacoesRepository(SistemaControleContext context)
        => _context = context;

    public async Task CriarAsync(Transacao transacoes, CancellationToken ct)
        => await _context.Set<Transacao>().AddAsync(transacoes, ct);

    public async Task<IEnumerable<Transacao>> ListarTodosAsync(CancellationToken ct)
        => await _context.Set<Transacao>().AsNoTracking().ToListAsync(ct);

    public async Task<Categoria?> ObterCategoriaPorIdAsync(Guid categoriaId, CancellationToken ct)
        => await _context.Set<Categoria>().AsNoTracking().FirstOrDefaultAsync(c => c.Id.Equals(categoriaId), ct);

    public async Task<Pessoa?> ObterPessoaPorIdAsync(Guid pessoaId, CancellationToken ct)
        => await _context.Set<Pessoa>().AsNoTracking().FirstOrDefaultAsync(c => c.Id.Equals(pessoaId), ct);
}