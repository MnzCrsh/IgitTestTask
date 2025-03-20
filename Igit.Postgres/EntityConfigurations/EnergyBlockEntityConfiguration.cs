using Igit.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Igit.Postgres.EntityConfigurations;

internal class EnergyBlockEntityConfiguration : IEntityTypeConfiguration<EnergyBlock>
{
    public void Configure(EntityTypeBuilder<EnergyBlock> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.SensorCount)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.PlannedMaintenance)
            .IsRequired();

        builder.HasOne(x => x.Station)
            .WithMany(x => x.EnergyBlocks)
            .HasForeignKey(x => x.StationId);
    }
}