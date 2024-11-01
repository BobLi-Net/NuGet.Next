using NuGet.Versioning;

namespace NuGet.Next.Core
{
    public class PackageDatabase : IPackageDatabase
    {
        private readonly IContext _context;

        public PackageDatabase(IContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<PackageAddResult> AddAsync(Package package, CancellationToken cancellationToken)
        {
            try
            {
                package.TargetFrameworks.ForEach(x=>x.PackageId = package.Id);
                package.Dependencies.ForEach(x=>x.PackageId = package.Id);
                
                
                _context.Packages.Add(package);

                await _context.SaveChangesAsync(cancellationToken);

                return PackageAddResult.Success;
            }
            catch (DbUpdateException e)
                when (_context.IsUniqueConstraintViolationException(e))
            {
                return PackageAddResult.PackageAlreadyExists;
            }
        }

        public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken)
        {
            return await _context
                .Packages
                .Where(p => p.Id == id)
                .AnyAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(string id, NuGetVersion version, CancellationToken cancellationToken)
        {
            return await _context
                .Packages
                .Where(p => p.Id == id)
                .Where(p => p.NormalizedVersionString == version.ToNormalizedString())
                .AnyAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Package>> FindAsync(string id, bool includeUnlisted,
            CancellationToken cancellationToken)
        {
            var query = _context.Packages
                .Include(p => p.Dependencies)
                .Include(p => p.PackageTypes)
                .Include(p => p.TargetFrameworks)
                .Where(p => p.Id == id);

            if (!includeUnlisted)
            {
                query = query.Where(p => p.Listed);
            }

            return (await query.ToListAsync(cancellationToken)).AsReadOnly();
        }

        public Task<Package> FindOrNullAsync(
            string id,
            NuGetVersion version,
            bool includeUnlisted,
            CancellationToken cancellationToken)
        {
            var query = _context.Packages
                .Include(p => p.Dependencies)
                .Include(p => p.TargetFrameworks)
                .Where(p => p.Id == id)
                .Where(p => p.NormalizedVersionString == version.ToNormalizedString());

            if (!includeUnlisted)
            {
                query = query.Where(p => p.Listed);
            }

            return query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> UnlistPackageAsync(string id, NuGetVersion version, CancellationToken cancellationToken)
        {
            var result = await _context.Packages
                .Where(x => x.Id == id && x.NormalizedVersionString == version.ToNormalizedString())
                .ExecuteUpdateAsync(x => x.SetProperty(i => i.Listed, false), cancellationToken);
            return result > 0;
        }

        public async Task<bool> RelistPackageAsync(string id, NuGetVersion version, CancellationToken cancellationToken)
        {
            var result = await _context.Packages
                .Where(x => x.Id == id && x.NormalizedVersionString == version.ToNormalizedString())
                .ExecuteUpdateAsync(x => x.SetProperty(i => i.Listed, true), cancellationToken);
            return result > 0;
        }

        public async Task AddDownloadAsync(string id, NuGetVersion version, CancellationToken cancellationToken)
        {
            await _context.Packages.Where(x => x.Id == id && x.NormalizedVersionString == version.ToNormalizedString())
                .ExecuteUpdateAsync(x => x.SetProperty(i => i.Downloads, i => i.Downloads + 1), cancellationToken);
        }

        public async Task<bool> HardDeletePackageAsync(string id, NuGetVersion version,
            bool isDelete,
            CancellationToken cancellationToken)
        {
            // 如果是需要直接删除则不会记录日志
            if (isDelete)
            {
                var result = await _context.Packages
                    .Where(p => p.Id == id)
                    .Where(p => p.NormalizedVersionString == version.ToNormalizedString())
                    .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                if (result > 0)
                {
                    await _context.PackageDependencies
                        .Where(d => d.PackageId == id)
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                    await _context.TargetFrameworks
                        .Where(t => t.PackageId == id)
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                    return true;
                }
            }
            else
            {
                var package = await _context.Packages
                    .Where(p => p.Id == id)
                    .Where(p => p.NormalizedVersionString == version.ToNormalizedString())
                    .Include(p => p.Dependencies)
                    .Include(p => p.TargetFrameworks)
                    .FirstOrDefaultAsync(cancellationToken);

                if (package == null)
                {
                    return false;
                }

                _context.Packages.Remove(package);
                await _context.SaveChangesAsync(cancellationToken);
            }


            return true;
        }

        public Task<bool> IsAuthorAsync(string id, string userId, CancellationToken cancellationToken)
        {
            return _context.Packages
                .Where(p => p.Id == id)
                .Where(p => p.Creator == userId)
                .AnyAsync(cancellationToken);
        }

        public Task<Package?> Find(string id, CancellationToken cancellationToken)
        {
            return _context.Packages
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}