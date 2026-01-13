namespace SistemaControle.Domain.Models.CategoriaAggregate;

public interface ICategoriaRepository
{
    Task<IEnumerable<Categoria>> ListarTodosAsync(CancellationToken ct);
    Task CriarAsync(Categoria categoria, CancellationToken ct);
}
