# SpecificationPattern

#### Implementation of a Data Access pattern for Entity Framework


## Description
This project maintains EF UnitOfWork (UoW) approach, but gets rid of repositories, using UoW directly.

`IUnitOfWork` interface exposes all the methods needed to query and modify data.

```Csharp
public interface IUnitOfWork
{
    ValueTask<TEntity?> GetById<TEntity>(object?[]? id, CancellationToken cancellationToken = default) 
		where TEntity : class;
    Task<List<TEntity>> List<TEntity>(CancellationToken cancellationToken = default) 
		where TEntity : class;
    Task<List<TProjection>> List<TEntity, TProjection>(Query<TEntity, TProjection> query, CancellationToken cancellationToken = default) 
		where TEntity : class;
    Task<TEntity?> FirstOrDefault<TEntity>(CancellationToken cancellationToken = default)
		where TEntity : class;
    Task<TProjection?> FirstOrDefault<TEntity, TProjection>(Query<TEntity, TProjection> query, CancellationToken cancellationToken = default)
		where TEntity : class;
    void Add<TEntity>(TEntity entity)
		where TEntity : class;
    void Update<TEntity>(TEntity entity)
		where TEntity : class;
    void Delete<TEntity>(TEntity entity)
		where TEntity : class;
}
```

To query data, the UoW uses the `Query` class, which represents the DB operation to read data. This way, it forces you to project the data to a specific class/dto, avoiding retrieving lots of data.

There are 3 query classes:
* `Query<TEntity, TProjection>`. Queries the TEntity DbSet and must return a TProjection item.
* `SelfQuery<TEntity>`. Queries the TEntity DbSet and must return the same TEntity.
* `PaginatedQuery<TEntity, TProjection>`. Has a constructor which receives a page number and page size, and paginates the response.

## Examples
In the Specification.Examples project there are example queries and a service to view the usage of both elements.

Example query:
```Csharp
public class GetPeopleByNameQuery : Query<Person, GetPeopleByNameQueryDto>
{
    public string Name { get; }

    public GetPeopleByNameQuery(string name)
    {
        Name = name;
    }

    public override IQueryable<GetPeopleByNameQueryDto> Apply(IQueryable<Person> baseQuery)
    {
        return baseQuery.Where(x => x.FirstName == Name)
            .Select(x => new GetPeopleByNameQueryDto(x.FirstName + " " + x.LastName, x.BirthDate));
    }
}

public record GetPeopleByNameQueryDto(string CompleteName, DateTime BirthDate);
```

Because all the logic is in the query, services don't need extra mapping, so they end up being:
```Csharp
public Task<List<GetCatsWithOwnersQueryDto>> GetCats()
{
    var query = new GetCatsWithOwnersQuery();

    return _unitOfWork.List(query);
}
```
