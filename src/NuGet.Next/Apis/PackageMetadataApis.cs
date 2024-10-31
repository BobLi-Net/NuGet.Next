using Gnarly.Data;
using NuGet.Next.Core;
using NuGet.Next.Protocol.Models;
using NuGet.Versioning;

namespace NuGet.Next.Service;

public class PackageMetadataApis(IPackageMetadataService metadata) : IScopeDependency
{
    
    public async Task<BaGetRegistrationIndexResponse> RegistrationIndexAsync(HttpContext context,string id)
    {
        var index = await metadata.GetRegistrationIndexOrNullAsync(id, context.RequestAborted);
        if (index == null)
        {
            context.Response.StatusCode = 404;

            return null;
        }

        return index;
    }

    public async Task<RegistrationLeafResponse> RegistrationLeafAsync(HttpContext context,string id, string version)
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
}