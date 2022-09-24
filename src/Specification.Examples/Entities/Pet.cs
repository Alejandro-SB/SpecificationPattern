using Specification.Examples.Enums;

namespace Specification.Examples.Entities;
public class Pet
{
    public int OwnerId { get; set; }
    public string Name { get; set; }
    public PetType Type { get; set; }

    public Person Owner { get; set; }
}
