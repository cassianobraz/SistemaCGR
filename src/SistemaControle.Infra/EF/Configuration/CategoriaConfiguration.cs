using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaControle.Domain.Models.CategoriaAggregate;

namespace SistemaControle.Infra.EF.Configuration;

public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(x => x.Finalidade)
            .IsRequired()
            .HasConversion<int>();
    }
}