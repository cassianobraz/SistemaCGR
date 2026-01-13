using Microsoft.EntityFrameworkCore;

namespace SistemaControle.Infra.EF.DbContext;

public class SistemaControleContext : Microsoft.EntityFrameworkCore.DbContext
{
    public SistemaControleContext(DbContextOptions<SistemaControleContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
