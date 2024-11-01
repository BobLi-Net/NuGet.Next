using Gnarly.Data;
using NuGet.Frameworks;
using NuGet.Next.Core;
using NuGet.Next.Protocol.Models;
using NuGet.Versioning;

namespace NuGet.Next.Service;

public class PackageMetadataApis(
    IPackageMetadataService metadata,
    IPackageService packageService,
    ISearchService searchService,
    IUrlGenerator urlGenerator,
    IPackageContentService contentService) : IScopeDependency
{
    public async Task<BaGetRegistrationIndexResponse> RegistrationIndexAsync(HttpContext context, string id)
    {
        var index = await metadata.GetRegistrationIndexOrNullAsync(id, context.RequestAborted);
        if (index == null)
        {
            context.Response.StatusCode = 404;

            return null;
        }

        return index;
    }

    public async Task<RegistrationLeafResponse> RegistrationLeafAsync(HttpContext context, string id, string version)
    {
        if (!NuGetVersion.TryParse(version, out var nugetVersion))
        {
            context.Response.StatusCode = 404;
            return null;
        }

        var leaf = await metadata.GetRegistrationLeafOrNullAsync(id, nugetVersion, context.RequestAborted);
        if (leaf == null)
        {
            context.Response.StatusCode = 404;
            return null;
        }

        return leaf;
    }

    public async Task<object> GetAsync(HttpContext context, string id, string version)
    {
        var packages = await packageService.FindPackagesAsync(id, context.RequestAborted);
        var listedPackages = packages.Where(p => p.Listed).ToList();
        Package package = null;
        var found = false;
        // Try to find the requested version.
        if (NuGetVersion.TryParse(version, out var requestedVersion))
        {
            package = packages.SingleOrDefault(p => p.Version == requestedVersion);
        }

        // Otherwise try to display the latest version.
        if (package == null)
        {
            package = listedPackages.OrderByDescending(p => p.Version).FirstOrDefault();
        }

        if (package == null)
        {
            package = new Package { Id = id };
            found = false;
            return new
            {
                package,
                found
            };
        }

        var packageVersion = package.Version;

        found = true;
        var isDotnetTemplate =
            package.PackageTypes.Any(t => t.Name.Equals("Template", StringComparison.OrdinalIgnoreCase));
        var isDotnetTool =
            package.PackageTypes.Any(t => t.Name.Equals("DotnetTool", StringComparison.OrdinalIgnoreCase));
        var lastUpdated = packages.Max(p => p.Published);
        var totalDownloads = packages.Sum(p => p.Downloads);

        var dependents = await searchService.FindDependentsAsync(package.Id, context.RequestAborted);

        var usedBy = dependents.Data;
        var dependencyGroups = ToDependencyGroups(package);
        var versions = ToVersions(listedPackages, packageVersion);

        var readme = string.Empty;
        if (package.HasReadme)
        {
            readme = await GetReadmeHtmlStringOrNullAsync(package.Id, packageVersion, context.RequestAborted);
        }

        var iconUrl = package.HasEmbeddedIcon
            ? urlGenerator.GetPackageIconDownloadUrl(package.Id, packageVersion)
            : package.IconUrlString;
        var licenseUrl = package.LicenseUrlString;
        var packageDownloadUrl = urlGenerator.GetPackageDownloadUrl(package.Id, packageVersion);

        package.PackageTypes.ForEach(x=>x.Package = null);
        package.Dependencies.ForEach(x=>x.Package = null);
        package.TargetFrameworks.ForEach(x=>x.Package = null);
        
        return new
        {
            package,
            found,
            isDotnetTemplate,
            isDotnetTool,
            lastUpdated,
            totalDownloads,
            usedBy,
            dependencyGroups,
            versions,
            readme,
            iconUrl,
            licenseUrl,
            packageDownloadUrl
        };
    }

    private async Task<string> GetReadmeHtmlStringOrNullAsync(
        string packageId,
        NuGetVersion packageVersion,
        CancellationToken cancellationToken)
    {
        string readme;
        using (var readmeStream =
               await contentService.GetPackageReadmeStreamOrNullAsync(packageId, packageVersion, cancellationToken))
        {
            if (readmeStream == null) return null;

            using (var reader = new StreamReader(readmeStream))
            {
                readme = await reader.ReadToEndAsync(cancellationToken);
            }
        }

        return readme;
    }

    private IReadOnlyList<VersionModel> ToVersions(IReadOnlyList<Package> packages, NuGetVersion selectedVersion)
    {
        return packages
            .Select(p => new VersionModel
            {
                Version = p.Version,
                Downloads = p.Downloads,
                Selected = p.Version == selectedVersion,
                LastUpdated = p.Published,
            })
            .OrderByDescending(m => m.Version)
            .ToList();
    }

    private IReadOnlyList<DependencyGroupModel> ToDependencyGroups(Package package)
    {
        return package
            .Dependencies
            .GroupBy(d => d.TargetFramework)
            .Select(group =>
            {
                return new DependencyGroupModel
                {
                    Name = PrettifyTargetFramework(group.Key),
                    Dependencies = group
                        .Where(d => d.Id != null)
                        .Select(d => new DependencyModel
                        {
                            PackageId = d.Id,
                            VersionSpec = (d.VersionRange != null)
                                ? VersionRange.Parse(d.VersionRange).PrettyPrint()
                                : string.Empty
                        })
                        .ToList()
                };
            })
            .ToList();
    }


    private string PrettifyTargetFramework(string targetFramework)
    {
        if (targetFramework == null) return "All Frameworks";

        NuGetFramework framework;
        try
        {
            framework = NuGetFramework.Parse(targetFramework);
        }
        catch (Exception)
        {
            return targetFramework;
        }

        string frameworkName;
        if (framework.Framework.Equals(FrameworkConstants.FrameworkIdentifiers.NetCoreApp,
                StringComparison.OrdinalIgnoreCase))
        {
            frameworkName = (framework.Version.Major >= 5)
                ? ".NET"
                : ".NET Core";
        }
        else if (framework.Framework.Equals(FrameworkConstants.FrameworkIdentifiers.NetStandard,
                     StringComparison.OrdinalIgnoreCase))
        {
            frameworkName = ".NET Standard";
        }
        else if (framework.Framework.Equals(FrameworkConstants.FrameworkIdentifiers.Net,
                     StringComparison.OrdinalIgnoreCase))
        {
            frameworkName = ".NET Framework";
        }
        else
        {
            frameworkName = framework.Framework;
        }

        var frameworkVersion = (framework.Version.Build == 0)
            ? framework.Version.ToString(2)
            : framework.Version.ToString(3);

        return $"{frameworkName} {frameworkVersion}";
    }

    public class DependencyGroupModel
    {
        public string Name { get; set; }
        public IReadOnlyList<DependencyModel> Dependencies { get; set; }
    }

    public class DependencyModel
    {
        public string PackageId { get; set; }
        public string VersionSpec { get; set; }
    }


    // TODO: Convert this to records.
    public class VersionModel
    {
        public NuGetVersion Version { get; set; }
        public long Downloads { get; set; }
        public bool Selected { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}