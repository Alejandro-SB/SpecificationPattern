namespace Specification.Abstractions;
public abstract class PaginatedQuery<TEntity, TProjection> : Query<TEntity, TProjection>
{
    public int Page { get; set; }
    public int Size { get; set; }

    public PaginatedQuery(int page, int size)
    {
        Page = page;
        Size = size;
    }
}
