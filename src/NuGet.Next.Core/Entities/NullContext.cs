using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace NuGet.Next.Core;

public class NullContext : IContext
{
    public DatabaseFacade Database => throw new NotImplementedException();

    public DbSet<Package> Packages
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public DbSet<User> Users
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public DbSet<UserKey> UserKeys
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public DbSet<PackageDependency> PackageDependencies { get; set; }
    public DbSet<TargetFramework> TargetFrameworks { get; set; }

    public DbSet<PackageUpdateRecord> PackageUpdateRecords { get; set; }

    public bool SupportsLimitInSubqueries => throw new NotImplementedException();

    public bool IsUniqueConstraintViolationException(DbUpdateException exception)
    {
        throw new NotImplementedException();
    }

    public Task RunMigrationsAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}