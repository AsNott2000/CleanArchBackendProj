using System;
using BuildingBlocks.Domain.Interfaces;

namespace CleanArchProject.Domain.Interfaces;

public interface IUnitOfWork : IBaseUnitOfWork
{
    public ICarModelRepository CarsRepository { get; init; }
    public IUserModelRepository UsersRepository { get; init; }
}
