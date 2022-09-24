using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Specification.Examples.Entities;

namespace Specification.Examples.Data;
public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("Pets");

        builder.Property(x => x.Name);
        builder.Property(x => x.Type);
        builder.Property(x => x.OwnerId);

        builder.HasOne(x => x.Owner)
            .WithMany(x => x.Pets)
            .HasForeignKey(x => x.OwnerId);
    }
}
