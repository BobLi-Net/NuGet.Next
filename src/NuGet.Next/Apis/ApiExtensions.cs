using Microsoft.AspNetCore.Mvc;
using NuGet.Next.Options;

namespace NuGet.Next.Service;

public static class ApiExtensions
{
    public static IEndpointRouteBuilder MapApis(this IEndpointRouteBuilder app)
    {
        var options = app.ServiceProvider.GetRequiredService<NuGetNextOptions>();

        var group = app.MapGroup(options.PathBase ?? "/");

        group.Map("/v3/package/{id}/index.json",
                async (PackageApis apis, HttpContext context, string id) =>
                    await apis.GetPackageVersionsAsync(context, id))
            .WithOpenApi();

        group.Map("v3/package/{id}/{version}/{idVersion}.nupkg",
                async (PackageApis apis, HttpContext context, string id, string version) =>
                    await apis.DownloadPackageAsync(context, id, version))
            .WithOpenApi();

        group.Map("v3/package/{id}/{version}/{id2}.nuspec",
                async (PackageApis apis, HttpContext context, string id, string version) =>
                    await apis.DownloadNuspecAsync(context, id, version))
            .WithOpenApi();

        group.Map("v3/package/{id}/{version}/readme",
                async (PackageApis apis, HttpContext context, string id, string version) =>
                    await apis.DownloadReadmeAsync(context, id, version))
            .WithOpenApi();

        group.Map("v3/package/{id}/{version}/icon",
                async (PackageApis apis, HttpContext context, string id, string version) =>
                    await apis.DownloadIconAsync(context, id, version))
            .WithOpenApi();

        group.Map("/v3/registration/{id}/index.json",
                async (PackageMetadataApis apis, HttpContext context, string id) =>
                    await apis.RegistrationIndexAsync(context, id))
            .WithOpenApi();

        group.Map("/v3/registration/{id}/{version}.json",
                async (PackageMetadataApis apis, HttpContext context, string id, string version) =>
                    await apis.RegistrationLeafAsync(context, id, version))
            .WithOpenApi();

        group.MapPut("api/v2/package",
                async (PackagePublishApis apis, HttpContext context) =>
                    await apis.UploadAsync(context))
            .WithOpenApi();

        group.MapDelete("api/v2/package/{id}/{version}",
                async (PackagePublishApis apis, HttpContext context, string id, string version) =>
                    await apis.Delete(context, id, version))
            .WithOpenApi();

        group.MapPost("api/v2/package/{id}/{version}",
                async (PackagePublishApis apis, HttpContext context, string id, string version) =>
                    await apis.Delete(context, id, version))
            .WithOpenApi();

        group.Map("v3/search",
                async (SearchApis apis, HttpContext context,
                        [FromQuery(Name = "q")] string? query = null,
                        [FromQuery] int skip = 0,
                        [FromQuery] int take = 20,
                        [FromQuery] bool prerelease = false,
                        [FromQuery] string? semVerLevel = null,
                        [FromQuery] string? packageType = null,
                        [FromQuery] string? framework = null) =>
                    await apis.SearchAsync(context, query, skip, take, prerelease, semVerLevel, packageType, framework))
            .WithOpenApi();

        group.Map("v3/autocomplete",
                async (SearchApis apis, HttpContext context,
                        [FromQuery(Name = "q")] string? autocompleteQuery = null,
                        [FromQuery(Name = "id")] string? versionsQuery = null,
                        [FromQuery] bool prerelease = false,
                        [FromQuery] string? semVerLevel = null,
                        [FromQuery] int skip = 0,
                        [FromQuery] int take = 20,

                        // These are unofficial parameters
                        [FromQuery] string? packageType = null) =>
                    await apis.AutocompleteAsync(autocompleteQuery, versionsQuery, prerelease, semVerLevel, skip, take,
                        packageType, context.RequestAborted))
            .WithOpenApi();

        group.Map("v3/dependents",
                async (SearchApis apis,
                        HttpContext context,
                        [FromQuery] string? packageId = null) =>
                    await apis.DependentsAsync(context, packageId))
            .WithOpenApi();

        group.MapGet("v3/index.json",
                async (ServiceIndexService apis, HttpContext context) =>
                    await apis.GetAsync(context.RequestAborted))
            .WithOpenApi();
        
        group.MapPut("api/v2/symbol",
                async (SymbolApis apis, HttpContext context) =>
                    await apis.Upload(context))
            .WithOpenApi();
        
        group.MapGet("api/download/symbols/{file}/{key}/{file2}",
                async (SymbolApis apis, HttpContext context, string file, string key, string file2) =>
                    await apis.Get(context, file, key))
            .WithOpenApi();

        return app;
    }
}