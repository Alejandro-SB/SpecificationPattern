using Microsoft.EntityFrameworkCore;
using Specification.Examples.Entities;
using System.Reflection;

namespace Specification.Examples.Data;
public class PetDbContext : DbContext
{
    public PetDbContext(DbContextOptions<PetDbContext> options) : base(options)
    {

    }

    public DbSet<Person> People { get; set; } = null!;
    public DbSet<Pet> Pets { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
