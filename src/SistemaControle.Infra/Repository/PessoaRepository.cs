using Microsoft.EntityFrameworkCore;
using SistemaControle.Domain.Models.PessoaAggregate;
using SistemaControle.Domain.Shared.Enums;
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

    public async Task<ConsultaTotaisPorPessoa> ConsultarTotaisPorPessoaAsync(CancellationToken ct)
    {
        var porPessoa = await _context.Pessoas
            .AsNoTracking()
            .Select(p => new PessoaTotais(
                p.Id,
                p.Nome,
                _context.Transacoes
                    .Where(t => t.PessoaId == p.Id && t.Tipo == TipoTransacao.Receita)
                    .Select(t => (decimal?)t.Valor)
                    .Sum() ?? 0m,
                _context.Transacoes
                    .Where(t => t.PessoaId == p.Id && t.Tipo == TipoTransacao.Despesa)
                    .Select(t => (decimal?)t.Valor)
                    .Sum() ?? 0m
            ))
            .ToListAsync(ct);

        var totalReceitasGeral = porPessoa.Sum(x => x.TotalReceitas);
        var totalDespesasGeral = porPessoa.Sum(x => x.TotalDespesas);

        return new ConsultaTotaisPorPessoa(
            Pessoas: porPessoa,
            TotalReceitasGeral: totalReceitasGeral,
            TotalDespesasGeral: totalDespesasGeral
        );
    }
}