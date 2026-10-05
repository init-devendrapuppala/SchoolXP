using IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure.Persistence;

public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : DbContext(options)
{
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
    public DbSet<UserIdentity> UserIdentities => Set<UserIdentity>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(userProfile => userProfile.UserId);
            entity.Property<Guid>(userProfile => userProfile.UserId)
                .HasDefaultValueSql("gen_random_uuid()");
            entity.Property<string>(userProfile => userProfile.Email).HasMaxLength(50).IsRequired();
            entity.HasIndex(userProfile => userProfile.Email).IsUnique();
            entity.Property<string>(userProfile => userProfile.FirstName).HasMaxLength(50).IsRequired();
            entity.Property<string>(userProfile => userProfile.LastName).HasMaxLength(50).IsRequired();
            entity.Property<bool>(userProfile => userProfile.isActive).IsRequired();
            entity.Property(userProfile => userProfile.IsPlatformAdmin)
                .HasDefaultValue(false)
                .IsRequired();
            entity.Property<DateTime>(userProfile => userProfile.CreatedAtUtc);
            entity.Property<DateTime?>(userProfile => userProfile.UpdatedAtUtc);
        });

        modelBuilder.Entity<UserIdentity>(entity =>
        {
            entity.HasKey(identity => new
            {
                identity.UserId,
                identity.Sub
            });

            entity.Property(identity => identity.Sub)
                .HasMaxLength(200)
                .IsRequired();

            entity.HasOne<UserProfile>()
                .WithMany(user => user.UserIdentities)
                .HasForeignKey(identity => identity.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(identity => identity.Sub)
                .IsUnique();
        });
    }
}