using Result.Domain.Models;

namespace SistemaControle.Domain.Services.Interfaces;

public interface IPessoaService
{
    Task<ResultViewModel<Guid>> CriarAsync(string nome, int idade, CancellationToken ct);
    Task<ResultViewModel<bool>> ExcluirAsync(Guid id, CancellationToken ct);
}