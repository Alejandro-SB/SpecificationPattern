﻿namespace Specification.Examples.Entities;
public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }

    public ICollection<Pet> Pets { get; set; } = new List<Pet>();
}
