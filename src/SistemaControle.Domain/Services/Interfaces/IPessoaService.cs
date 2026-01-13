using SistemaControle.Domain.Models.PessoaAggregate;

namespace SistemaControle.Domain.Services.Interfaces;

public interface IPessoaService
{
    Task CriarAsync(Pessoa pessoa, CancellationToken ct);
    void Excluir(Pessoa pessoa);
}
