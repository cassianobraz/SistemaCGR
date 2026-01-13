using SistemaControle.Domain.Models.PessoaAggregate;
using SistemaControle.Domain.Services.Interfaces;

namespace SistemaControle.Domain.Services;

public class PessoaService : IPessoaService
{
    public Task CriarAsync(Pessoa pessoa, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public void Excluir(Pessoa pessoa)
    {
        throw new NotImplementedException();
    }
}
