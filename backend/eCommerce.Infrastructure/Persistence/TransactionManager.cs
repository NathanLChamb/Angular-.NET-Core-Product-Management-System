using eCommerce.Application.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace eCommerce.Infrastructure.Persistence
{
    public class TransactionManager : ITransactionManager
    {
        private readonly eCommerceContext _context;

        private IDbContextTransaction? _transaction;
        public TransactionManager(eCommerceContext context)
        {
            _context = context;
        }
        public async Task BeginTransactionAsync(CancellationToken ct = default)
        {
            _transaction = await _context.Database.BeginTransactionAsync(ct);
        }
        public async Task CommitTransactionAsync(CancellationToken ct = default)
        {
            if (_transaction == null) return;

            await _transaction.CommitAsync(ct);
            await _transaction.DisposeAsync();

            _transaction = null;
        }


        public async Task RollbackTransactionAsync(CancellationToken ct = default)
        {
            if (_transaction == null) return;

            await _transaction.RollbackAsync(ct);
            await _transaction.DisposeAsync();

            _transaction = null;
        }
    }
}