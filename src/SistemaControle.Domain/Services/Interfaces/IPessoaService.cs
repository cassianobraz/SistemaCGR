using Result.Domain.Models;
using SistemaControle.Domain.Models.PessoaAggregate;

namespace SistemaControle.Domain.Services.Interfaces;

public interface IPessoaService
{
    Task<ResultViewModel<bool>> CriarAsync(string nome, int idade, CancellationToken ct);
    Task<ResultViewModel<bool>> ExcluirAsync(Guid id, CancellationToken ct);
}