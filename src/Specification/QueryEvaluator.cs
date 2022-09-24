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
        
        var queryResult = query.Apply(queryWithStringIncludes);

        if (query is PaginatedQuery<TEntity, TProjection> paginate)
        {
            return queryResult.Skip(paginate.Size * paginate.Page).Take(paginate.Size);
        }

        return queryResult;
    }
}
