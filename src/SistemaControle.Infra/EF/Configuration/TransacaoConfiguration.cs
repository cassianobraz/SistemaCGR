using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaControle.Domain.Models.TransacoesAggregate;

namespace SistemaControle.Infra.EF.Configuration;

public class TransacaoConfiguration : IEntityTypeConfiguration<Transacoes>
{
    public void Configure(EntityTypeBuilder<Transacoes> builder)
    {
        throw new NotImplementedException();
    }
}