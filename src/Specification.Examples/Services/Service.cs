using Specification.Abstractions;
using Specification.Examples.Queries.People.GetPeopleByName;
using Specification.Examples.Queries.Pets.GetCatsWithOwners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Specification.Examples.Services;
public class Service
{
    private readonly IUnitOfWork _unitOfWork;

    public Service(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<List<GetCatsWithOwnersQueryDto>> GetCats()
    {
        var query = new GetCatsWithOwnersQuery();

        return _unitOfWork.List(query);
    }

    public Task<GetPeopleByNameQueryDto?> GetFirstByName(string name)
    {
        var query = new GetPeopleByNameQuery(name);

        return _unitOfWork.FirstOrDefault(query);
    }
}
