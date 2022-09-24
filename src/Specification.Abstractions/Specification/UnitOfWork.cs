using Microsoft.EntityFrameworkCore;
using Specification.Abstractions;

namespace Specification
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public ValueTask<TEntity?> GetById<TEntity>(object?[]? id, CancellationToken cancellationToken = default) where TEntity : class
        {
            return _context.FindAsync<TEntity>(id, cancellationToken: cancellationToken);
        }

        public Task<List<TEntity>> List<TEntity>(CancellationToken cancellationToken = default) where TEntity : class
        {
            return _context.Set<TEntity>().ToListAsync(cancellationToken);
        }

        public Task<List<TProjection>> List<TEntity, TProjection>(Query<TEntity, TProjection> query, CancellationToken cancellationToken = default) where TEntity : class
        {
            var queryProjection = QueryEvaluator.ApplyQuery(_context, query);

            return queryProjection.ToListAsync(cancellationToken);
        }

        public Task<TEntity?> FirstOrDefault<TEntity>(CancellationToken cancellationToken = default) where TEntity : class
        {
            return _context.Set<TEntity>().FirstOrDefaultAsync(cancellationToken);
        }

        public Task<TProjection?> FirstOrDefault<TEntity, TProjection>(Query<TEntity, TProjection> query, CancellationToken cancellationToken = default) where TEntity : class
        {
            var queryProjection = QueryEvaluator.ApplyQuery(_context, query);

            return queryProjection.FirstOrDefaultAsync(cancellationToken);
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Add(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Update(entity);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Remove(entity);
        }
    }
}