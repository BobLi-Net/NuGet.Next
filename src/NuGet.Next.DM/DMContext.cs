using Microsoft.EntityFrameworkCore;
using NuGet.Next.Core;

namespace NuGet.Next.DM;


public class DMContext : AbstractContext<DMContext>
{
    /// <summary>
    /// The MySQL Server error code for when a unique constraint is violated.
    /// </summary>
    private const int UniqueConstraintViolationErrorCode = 1062;

    public DMContext(DbContextOptions<DMContext> options, IServiceProvider serviceProvider) : base(options,
        serviceProvider)
    {
    }

    public override bool IsUniqueConstraintViolationException(DbUpdateException exception)
    {
        return false;
    }

    /// <summary>
    /// MySQL does not support LIMIT clauses in subqueries for certain subquery operators.
    /// See: https://dev.mysql.com/doc/refman/8.0/en/subquery-restrictions.html
    /// </summary>
    public override bool SupportsLimitInSubqueries => false;
}