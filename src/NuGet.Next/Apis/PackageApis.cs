using Gnarly.Data;
using NuGet.Next.Core;
using NuGet.Next.Protocol.Models;
using NuGet.Versioning;

namespace NuGet.Next.Service;

public class PackageApis(IPackageContentService content) : IScopeDependency
{
    public async Task<PackageVersionsResponse> GetPackageVersionsAsync(HttpContext context, string id)
    {
        var versions = await content.GetPackageVersionsOrNullAsync(id, context.RequestAborted);
        if (versions == null)
        {
            context.Response.StatusCode = 404;

            return null;
        }

        return versions;
    }

    public async Task DownloadPackageAsync(HttpContext context, string id, string version)
    {
        if (!NuGetVersion.TryParse(version, out var nugetVersion))
        {
            context.Response.StatusCode = 404;
            return;
        }

        var packageStream = await content.GetPackageContentStreamOrNullAsync(id, nugetVersion, context.RequestAborted);
        if (packageStream == null)
        {
            context.Response.StatusCode = 404;
            return;
        }

        context.Response.ContentType = "application/octet-stream";

        await packageStream.CopyToAsync(context.Response.Body, context.RequestAborted);
    }

    public async Task DownloadNuspecAsync(HttpContext context, string id, string version)
    {
        if (!NuGetVersion.TryParse(version, out var nugetVersion))
        {
            context.Response.StatusCode = 404;
            return;
        }

        var nuspecStream = await content.GetPackageManifestStreamOrNullAsync(id, nugetVersion, context.RequestAborted);
        if (nuspecStream == null)
        {
            context.Response.StatusCode = 404;
            return;
        }

        context.Response.ContentType = "text/xml";

        await nuspecStream.CopyToAsync(context.Response.Body, context.RequestAborted);
    }

    public async Task DownloadReadmeAsync(HttpContext context, string id, string version)
    {
        if (!NuGetVersion.TryParse(version, out var nugetVersion))
        {
            context.Response.StatusCode = 404;
            return;
        }

        var readmeStream = await content.GetPackageReadmeStreamOrNullAsync(id, nugetVersion, context.RequestAborted);
        if (readmeStream == null)
        {
            context.Response.StatusCode = 404;
            return;
        }

        context.Response.ContentType = "text/markdown";
        await readmeStream.CopyToAsync(context.Response.Body, context.RequestAborted);
    }

    public async Task DownloadIconAsync(HttpContext context, string id, string version)
    {
        if (!NuGetVersion.TryParse(version, out var nugetVersion))
        {
            context.Response.StatusCode = 404;
            return;
        }

        var iconStream = await content.GetPackageIconStreamOrNullAsync(id, nugetVersion, context.RequestAborted);
        if (iconStream == null)
        {
            context.Response.StatusCode = 404;
            return;
        }

        context.Response.ContentType = "image/xyz";
        await iconStream.CopyToAsync(context.Response.Body, context.RequestAborted);
    }
}