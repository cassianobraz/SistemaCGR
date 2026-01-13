using Microsoft.EntityFrameworkCore;
using SistemaControle.Infra.EF.Configuration;

namespace SistemaControle.Infra.EF.DbContext;

public class SistemaControleContext : Microsoft.EntityFrameworkCore.DbContext
{
    public SistemaControleContext(DbContextOptions<SistemaControleContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
        modelBuilder.ApplyConfiguration(new PessoaConfiguration());
        modelBuilder.ApplyConfiguration(new TransacaoConfiguration());
    }
}