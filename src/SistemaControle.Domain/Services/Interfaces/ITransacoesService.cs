using Result.Domain.Models;
using SistemaControle.Domain.Shared.Enums;

namespace SistemaControle.Domain.Services.Interfaces;

public interface ITransacoesService
{
    Task<ResultViewModel<bool>> CriarAsync(string descricao, 
                                           decimal valor, 
                                           TipoTransacao tipo, 
                                           Guid categoriaId, 
                                           Guid pessoaId, 
                                           CancellationToken ct);
}