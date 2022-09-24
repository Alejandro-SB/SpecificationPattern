using Specification.Abstractions;
using Specification.Examples.Entities;

namespace Specification.Examples.Queries.People.GetPeopleByName;
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
