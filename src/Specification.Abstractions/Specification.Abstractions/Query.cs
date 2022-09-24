using System.Linq.Expressions;

namespace Specification.Abstractions;

public abstract class Query<TEntity, TProjection>
{
    private readonly List<Expression<Func<TEntity, object>>> _includes = new();
    private readonly List<string> _stringIncludes = new();

    public void Include(Expression<Func<TEntity, object>> predicate)
    {
        _includes.Add(predicate);
    }

    public void Include(string predicate)
    {
        _stringIncludes.Add(predicate);
    }

    public abstract IQueryable<TProjection> Apply(IQueryable<TEntity> baseQuery);

    public IReadOnlyList<Expression<Func<TEntity, object>>> Includes => _includes.AsReadOnly();
    public IReadOnlyList<string> StringIncludes => _stringIncludes.AsReadOnly();
}
