using Microsoft.EntityFrameworkCore;
using Specification.Abstractions;

namespace Specification;

internal static class QueryEvaluator
{
    internal static IQueryable<TProjection> ApplyQuery<TEntity, TProjection>(DbContext context, Query<TEntity, TProjection> query)
        where TEntity : class
    {
        var baseQuery = context.Set<TEntity>();

        var queryWithIncludes = query.Includes.Aggregate(baseQuery, (acc, q) => (DbSet<TEntity>)acc.Include(q));
        var queryWithStringIncludes = query.StringIncludes.Aggregate(queryWithIncludes, (acc, q) => (DbSet<TEntity>)acc.Include(q));

        return query.Apply(queryWithStringIncludes);
    }
}
