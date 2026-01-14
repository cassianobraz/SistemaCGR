using Microsoft.EntityFrameworkCore;
using SistemaControle.Domain.Models.CategoriaAggregate;
using SistemaControle.Domain.Models.PessoaAggregate;
using SistemaControle.Domain.Models.TransacoesAggregate;
using SistemaControle.Infra.EF.Configuration;

namespace SistemaControle.Infra.EF.DbContext;

public class SistemaControleContext : Microsoft.EntityFrameworkCore.DbContext
{
    public SistemaControleContext(DbContextOptions<SistemaControleContext> options) : base(options) { }

    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Transacao> Transacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
        modelBuilder.ApplyConfiguration(new PessoaConfiguration());
        modelBuilder.ApplyConfiguration(new TransacaoConfiguration());
    }
}