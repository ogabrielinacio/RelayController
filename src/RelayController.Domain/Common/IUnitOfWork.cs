﻿namespace RelayController.Domain.Common;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}