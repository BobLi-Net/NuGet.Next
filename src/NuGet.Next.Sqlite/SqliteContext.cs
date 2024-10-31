using NuGet.Next;
using Microsoft.EntityFrameworkCore;
using NuGet.Next.Core;

namespace NuGet.Next.Sqlite;

public class SqliteContext : AbstractContext<SqliteContext>
{
    /// <summary>
    /// The Sqlite error code for when a unique constraint is violated.
    /// </summary>
    private const int SqliteUniqueConstraintViolationErrorCode = 19;

    public SqliteContext(DbContextOptions<SqliteContext> options)
        : base(options)
    {
    }

    public override bool IsUniqueConstraintViolationException(DbUpdateException exception)
    {
        return false;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Package>()
            .Property(p => p.Id)
            .HasColumnType("TEXT COLLATE NOCASE");

        builder.Entity<Package>()
            .Property(p => p.NormalizedVersionString)
            .HasColumnType("TEXT COLLATE NOCASE");

        builder.Entity<PackageDependency>()
            .Property(d => d.Id)
            .HasColumnType("TEXT COLLATE NOCASE");

        builder.Entity<PackageType>()
            .Property(t => t.Name)
            .HasColumnType("TEXT COLLATE NOCASE");

        builder.Entity<TargetFramework>()
            .Property(f => f.Moniker)
            .HasColumnType("TEXT COLLATE NOCASE");

        builder.Entity<User>((option) =>
        {
            option.HasKey(x => x.Id);

            option.HasIndex(x => x.Username).IsUnique();

            option.Property(x => x.Username).IsRequired();

            option.Property(x => x.Email).IsRequired(false);
        });

        builder.Entity<UserKey>(option =>
        {
            option.HasKey(x => x.Id);
            
            option.Property(x=>x.Key).ValueGeneratedOnAdd();
            
            option.Property(x => x.CreatedTime).IsRequired();
            
            option.Property(x => x.UserId).IsRequired();
            
            // 配置逻辑外键
            option.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
            
            option.HasIndex(x => x.Key).IsUnique();
            
            option.HasIndex(x => x.UserId).IsUnique();
        });
    }
}