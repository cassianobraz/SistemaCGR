using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaControle.Domain.Model.PessoaAggregate;

namespace SistemaControle.Infra.EF.Configuration;

public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        throw new NotImplementedException();
    }
}
