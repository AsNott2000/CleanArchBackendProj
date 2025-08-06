using System;

namespace BuildingBlocks.Domain.Interfaces;

public interface IBaseUnitOfWork
{
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);

}
