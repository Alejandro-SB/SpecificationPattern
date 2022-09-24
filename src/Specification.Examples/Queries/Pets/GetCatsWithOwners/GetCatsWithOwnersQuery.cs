using Specification.Abstractions;
using Specification.Examples.Entities;
using Specification.Examples.Enums;

namespace Specification.Examples.Queries.Pets.GetCatsWithOwners;
public class GetCatsWithOwnersQuery : Query<Pet, GetCatsWithOwnersQueryDto>
{
    public override IQueryable<GetCatsWithOwnersQueryDto> Apply(IQueryable<Pet> query)
    {
        return query.Where(x => x.Type == PetType.Cat)
            .Select(x => new GetCatsWithOwnersQueryDto(x.Name, x.Owner.FirstName + " " + x.Owner.LastName));
    }
}

public record GetCatsWithOwnersQueryDto(string PetName, string OwnerName);