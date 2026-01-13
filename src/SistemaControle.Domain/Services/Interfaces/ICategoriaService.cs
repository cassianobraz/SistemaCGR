using Result.Domain.Models;

namespace SistemaControle.Domain.Services.Interfaces;

public interface ICategoriaService
{
    Task<ResultViewModel<bool>> CriarAsync(string descricao, string finalidade, CancellationToken ct);
}
