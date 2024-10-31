using Gnarly.Data;
using Microsoft.AspNetCore.Mvc;
using NuGet.Next.Core;
using NuGet.Next.Protocol.Models;

namespace NuGet.Next.Service;

public class SearchApis(ISearchService searchService) : IScopeDependency
{
    public async Task<SearchResponse> SearchAsync(
        HttpContext context,
        [FromQuery(Name = "q")] string query = null,
        [FromQuery] int skip = 0,
        [FromQuery] int take = 20,
        [FromQuery] bool prerelease = false,
        [FromQuery] string? semVerLevel = null,

        [FromQuery] string? packageType = null,
        [FromQuery] string? framework = null)
    {
        var request = new SearchRequest
        {
            Skip = skip,
            Take = take,
            IncludePrerelease = prerelease,
            IncludeSemVer2 = semVerLevel == "2.0.0",
            PackageType = packageType,
            Framework = framework,
            Query = query ?? string.Empty,
        };

        return await searchService.SearchAsync(request, context.RequestAborted);
    }

    public async Task<AutocompleteResponse> AutocompleteAsync(
        [FromQuery(Name = "q")] string? autocompleteQuery = null,
        [FromQuery(Name = "id")] string? versionsQuery = null,
        [FromQuery] bool prerelease = false,
        [FromQuery] string? semVerLevel = null,
        [FromQuery] int skip = 0,
        [FromQuery] int take = 20,

        // These are unofficial parameters
        [FromQuery] string packageType = null,
        CancellationToken cancellationToken = default)
    {
        // If only "id" is provided, find package versions. Otherwise, find package IDs.
        if (versionsQuery != null && autocompleteQuery == null)
        {
            var request = new VersionsRequest
            {
                IncludePrerelease = prerelease,
                IncludeSemVer2 = semVerLevel == "2.0.0",
                PackageId = versionsQuery,
            };

            return await searchService.ListPackageVersionsAsync(request, cancellationToken);
        }
        else
        {
            var request = new AutocompleteRequest
            {
                IncludePrerelease = prerelease,
                IncludeSemVer2 = semVerLevel == "2.0.0",
                PackageType = packageType,
                Skip = skip,
                Take = take,
                Query = autocompleteQuery,
            };

            return await searchService.AutocompleteAsync(request, cancellationToken);
        }
    }

    public async Task<DependentsResponse> DependentsAsync(
        HttpContext context,
        [FromQuery] string? packageId = null)
    {
        if (string.IsNullOrWhiteSpace(packageId))
        {
            context.Response.StatusCode = 400;
            return null;
        }

        return await searchService.FindDependentsAsync(packageId, context.RequestAborted);
    }
}