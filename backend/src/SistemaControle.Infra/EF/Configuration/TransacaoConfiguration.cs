using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaControle.Domain.Models.TransacoesAggregate;

public class TransacaoConfiguration : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(x => x.Valor)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.Tipo)
            .IsRequired()
            .HasConversion<int>();

        builder.HasOne(x => x.Pessoa)
            .WithMany(p => p.Transacoes)
            .HasForeignKey(x => x.PessoaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Categoria)
            .WithMany()
            .HasForeignKey(x => x.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.PessoaId);
        builder.HasIndex(x => x.CategoriaId);
    }
}