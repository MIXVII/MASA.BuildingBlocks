﻿namespace MASA.BuildingBlocks.Data.Uow;
public interface IUnitOfWork : IAsyncDisposable
{
    bool DisableRollbackOnFailure { get; set; }

    Task SaveChangesAsync(CancellationToken cancellationToken = default);

    Task CommitAsync(CancellationToken cancellationToken = default);

    Task RollbackAsync(CancellationToken cancellationToken = default);
}