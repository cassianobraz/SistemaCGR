using Microsoft.EntityFrameworkCore.Storage;
using SistemaControle.Domain.Shared;
using SistemaControle.Infra.EF.DbContext;

namespace SistemaControle.Infra.Repository;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly SistemaControleContext _context;
    private IDbContextTransaction _currentTransaction;

    public UnitOfWork(SistemaControleContext context)
        => _context = context;

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            await _currentTransaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            if (_currentTransaction != null)
                await _currentTransaction.RollbackAsync(cancellationToken);
        }
        finally
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);

    public void Dispose()
    {
        _currentTransaction?.Dispose();
        _context.Dispose();
    }
}
