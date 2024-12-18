using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace NuGet.Next.Core;

public interface IContext
{
    DatabaseFacade Database { get; }

    DbSet<Package> Packages { get; set; }
    
    DbSet<User> Users { get; set; }
    
    DbSet<UserKey> UserKeys { get; set; }
    
    DbSet<PackageDependency> PackageDependencies { get; set; }
    
    DbSet<TargetFramework> TargetFrameworks { get; set; }
    
    DbSet<PackageUpdateRecord> PackageUpdateRecords { get; set; }

    /// <summary>
    ///     Whether this database engine supports LINQ "Take" in subqueries.
    /// </summary>
    bool SupportsLimitInSubqueries { get; }

    /// <summary>
    ///     Check whether a <see cref="DbUpdateException" /> is due to a SQL unique constraint violation.
    /// </summary>
    /// <param name="exception">The exception to inspect.</param>
    /// <returns>Whether the exception was caused to SQL unique constraint violation.</returns>
    bool IsUniqueConstraintViolationException(DbUpdateException exception);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Applies any pending migrations for the context to the database.
    ///     Creates the database if it does not already exist.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the task.</param>
    /// <returns>A task that completes once migrations are applied.</returns>
    Task RunMigrationsAsync(CancellationToken cancellationToken);
}