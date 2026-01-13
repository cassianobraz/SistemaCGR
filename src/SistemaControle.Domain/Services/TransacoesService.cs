using SistemaControle.Domain.Models.TransacoesAggregate;
using SistemaControle.Domain.Services.Interfaces;

namespace SistemaControle.Domain.Services;

public class TransacoesService : ITransacoesService
{
    public Task CriarAsync(Transacoes transacoes, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
