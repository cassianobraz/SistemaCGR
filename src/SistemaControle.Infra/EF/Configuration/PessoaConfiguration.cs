using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaControle.Domain.Models.PessoaAggregate;

namespace SistemaControle.Infra.EF.Configuration;

public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever();

        builder.Property(p => p.Nome)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(p => p.Idade)
               .IsRequired();
    }
}