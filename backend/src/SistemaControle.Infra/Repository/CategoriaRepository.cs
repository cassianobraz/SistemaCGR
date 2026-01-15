using Microsoft.EntityFrameworkCore;
using SistemaControle.Domain.Models.CategoriaAggregate;
using SistemaControle.Domain.Shared.Enums;
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

    public async Task<ConsultaTotaisPorCategoria> ConsultarTotaisPorCategoriaAsync(CancellationToken ct)
    {
        var porCategoria = await _context.Categorias
            .AsNoTracking()
            .Select(c => new CategoriaTotais(
                c.Id,
                c.Descricao,
                _context.Transacoes
                    .Where(t => t.CategoriaId == c.Id && t.Tipo == TipoTransacao.Receita)
                    .Select(t => (decimal?)t.Valor)
                    .Sum() ?? 0m,
                _context.Transacoes
                    .Where(t => t.CategoriaId == c.Id && t.Tipo == TipoTransacao.Despesa)
                    .Select(t => (decimal?)t.Valor)
                    .Sum() ?? 0m
            ))
            .ToListAsync(ct);

        var totalReceitasGeral = porCategoria.Sum(x => x.TotalReceitas);
        var totalDespesasGeral = porCategoria.Sum(x => x.TotalDespesas);

        return new ConsultaTotaisPorCategoria(
            porCategoria,
            totalReceitasGeral,
            totalDespesasGeral
        );
    }
}