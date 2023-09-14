
using UfinetPrueba.Domain.Interfaces;

namespace UfinetPrueba.Infrastructure.Repository
{
    public class ApplicationDbContextUnitOfWork : IUnitOfWork
    {
        public ApplicationDbContext Context { get; }

        public ApplicationDbContextUnitOfWork(ApplicationDbContext context)
        {
            Context = context;
        }

        void IDisposable.Dispose()
        {
            Context.Dispose();
        }

        async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await Context.SaveChangesAsync(cancellationToken);
        }

        void IUnitOfWork.CommitTransaction()
        {
            Context.SaveChanges();
        }
    }
}
