﻿namespace Specification.Abstractions;

public interface IUnitOfWork
{
    ValueTask<TEntity?> GetById<TEntity>(object?[]? id, CancellationToken cancellationToken = default) where TEntity : class;
    Task<List<TEntity>> List<TEntity>(CancellationToken cancellationToken = default) where TEntity : class;
    Task<List<TProjection>> List<TEntity, TProjection>(Query<TEntity, TProjection> query, CancellationToken cancellationToken = default) where TEntity : class;
    Task<TEntity?> FirstOrDefault<TEntity>(CancellationToken cancellationToken = default) where TEntity : class;
    Task<TProjection?> FirstOrDefault<TEntity, TProjection>(Query<TEntity, TProjection> query, CancellationToken cancellationToken = default) where TEntity : class;
    void Add<TEntity>(TEntity entity) where TEntity : class;
    void Update<TEntity>(TEntity entity) where TEntity : class;
    void Delete<TEntity>(TEntity entity) where TEntity : class;
    Task<int> SaveChanges(CancellationToken cancellationToken = default);
}
