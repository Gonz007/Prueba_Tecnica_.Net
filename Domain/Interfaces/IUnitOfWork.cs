﻿namespace UfinetPrueba.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        void CommitTransaction();
    }
}
