using Result.Domain.Models;
using SistemaControle.Domain.Shared.Enums;

namespace SistemaControle.Domain.Services.Interfaces;

public interface ICategoriaService
{
    Task<ResultViewModel<bool>> CriarAsync(string descricao, Finalidade finalidade, CancellationToken ct);
}
