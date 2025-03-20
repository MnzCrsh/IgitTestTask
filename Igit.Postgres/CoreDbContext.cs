using Igit.Entities.Entities;
using Igit.Postgres.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Igit.Postgres;

/// <summary>
/// Primary database context
/// </summary>
public class CoreDbContext(DbContextOptions<CoreDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new StationEntityConfiguration());
        modelBuilder.ApplyConfiguration(new EnergyBlockEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());

        SeedSystemData(modelBuilder);
    }

    /// <summary>
    /// Seeds system roles and superuser
    /// </summary>
    private static void SeedSystemData(ModelBuilder modelBuilder)
    {
        const string adminGuid = "515CD069-5C8B-404B-8BD3-7573805573E6";
        
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = new Guid(adminGuid), Name = "Admin" },
            new Role { Id = new Guid("F0838FA3-A7AC-4D22-82C0-FDA5C2576B22"), Name = "User" });

        modelBuilder.Entity<User>().HasData(new User
        {
            Id = new Guid("8E9FB603-2396-42B6-872C-152663389D52"),
            FullName = "Superuser",
            Email = "superuser@mail.com",
            RoleId = new Guid(adminGuid),
        });
    }
}