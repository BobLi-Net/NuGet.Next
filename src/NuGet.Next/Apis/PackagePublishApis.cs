using Gnarly.Data;
using Microsoft.AspNetCore.Mvc;
using NuGet.Next.Core;
using NuGet.Next.Extensions;
using NuGet.Next.Options;
using NuGet.Versioning;

namespace NuGet.Next.Service;

public class PackagePublishApis(
    IAuthenticationService authentication,
    IPackageIndexingService indexer,
    IPackageDatabase packages,
    IPackageDeletionService deleteService,
    NuGetNextOptions options,
    ILogger<PackagePublishApis> logger) : IScopeDependency
{
    // See: https://docs.microsoft.com/en-us/nuget/api/package-publish-resource#push-a-package
    public async Task UploadAsync(HttpContext context)
    {
        if (options.IsReadOnlyMode ||
            !await authentication.AuthenticateAsync(context))
        {
            context.Response.StatusCode = 401;
            return;
        }

        using (var uploadStream = await context.GetUploadStreamOrNullAsync(context.RequestAborted))
        {
            if (uploadStream == null)
            {
                context.Response.StatusCode = 400;
                return;
            }

            var result = await indexer.IndexAsync(uploadStream, context.RequestAborted);

            switch (result)
            {
                case PackageIndexingResult.InvalidPackage:
                    context.Response.StatusCode = 400;
                    break;

                case PackageIndexingResult.PackageAlreadyExists:
                    context.Response.StatusCode = 409;
                    break;

                case PackageIndexingResult.Success:
                    context.Response.StatusCode = 201;
                    break;
            }
        }
    }

    public async Task Delete(HttpContext context, string id, string version)
    {
        if (options.IsReadOnlyMode)
        {
            context.Response.StatusCode = 401;
            return;
        }

        if (!NuGetVersion.TryParse(version, out var nugetVersion))
        {
            context.Response.StatusCode = 404;
            return;
        }

        if (!await authentication.AuthenticateAsync(context))
        {
            context.Response.StatusCode = 401;
            return;
        }

        if (await deleteService.TryDeletePackageAsync(id, nugetVersion, context.RequestAborted))
        {
            context.Response.StatusCode = 200;
        }
        else
        {
            context.Response.StatusCode = 404;
        }
    }

    [HttpPost]
    public async Task Relist(HttpContext context, string id, string version)
    {
        if (options.IsReadOnlyMode)
        {
            context.Response.StatusCode = 401;
            return;
        }

        if (!NuGetVersion.TryParse(version, out var nugetVersion))
        {
            context.Response.StatusCode = 404;
            return;
        }

        if (!await authentication.AuthenticateAsync(context))
        {
            context.Response.StatusCode = 401;
            return;
        }

        if (await packages.RelistPackageAsync(id, nugetVersion, context.RequestAborted))
        {
            context.Response.StatusCode = 200;
        }
        else
        {
            context.Response.StatusCode = 404;
        }
    }
}