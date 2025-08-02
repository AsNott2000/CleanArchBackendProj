using System;
using BuildingBlocks.Domain.Models;
using BuildingBlocks.Domain.SpecificationConfig;

namespace BuildingBlocks.Domain.Interfaces;

public interface IReadOnlyRepository<TEntity> where TEntity : Entity
{
    //arayüzü, generic ve sadece okuma (read-only) işlemleri için temel bir repository sözleşmesidir.
    //Entity’ler üzerinde sadece okuma işlemlerini (Get, List) standartlaştırır.
    //Herhangi bir entity tipiyle kullanılabilir (where TEntity : Entity).
    //Özellikle filtreli sorgular için Specification<TEntity> parametresiyle esnek ve güçlü sorgulama imkanı sunar.

    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity?> GetAsync(Specification<TEntity> specification, CancellationToken cancellationToken = default);
    Task<(int TotalCount, IReadOnlyList<TEntity> Data)> ListAsync(Specification<TEntity> specification, CancellationToken cancellationToken = default);
}

