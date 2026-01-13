using SistemaControle.Domain.Models.TransacoesAggregate;

namespace SistemaControle.Domain.Services.Interfaces;

public interface ITransacoesService
{
    Task CriarAsync(Transacoes transacoes, CancellationToken ct);
}
