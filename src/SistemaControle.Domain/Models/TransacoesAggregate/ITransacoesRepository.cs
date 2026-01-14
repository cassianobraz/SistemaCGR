using SistemaControle.Domain.Models.CategoriaAggregate;
using SistemaControle.Domain.Models.PessoaAggregate;

namespace SistemaControle.Domain.Models.TransacoesAggregate;

public interface ITransacoesRepository
{
    Task<IEnumerable<Transacao>> ListarTodosAsync(CancellationToken ct);
    Task CriarAsync(Transacao transacoes, CancellationToken ct);
    Task<Categoria?> ObterCategoriaPorIdAsync(Guid categoriaId, CancellationToken ct);
    Task<Pessoa?> ObterPessoaPorIdAsync(Guid pessoaId, CancellationToken ct);
}