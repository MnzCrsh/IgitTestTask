using Igit.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Igit.Postgres.EntityConfigurations;

/// <summary>
/// Entity type builder for <see cref="Station"/>
/// </summary>
internal class StationEntityConfiguration : IEntityTypeConfiguration<Station>
{
    public void Configure(EntityTypeBuilder<Station> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.EnergyBlocks).WithOne(x => x.Station);
    }
}