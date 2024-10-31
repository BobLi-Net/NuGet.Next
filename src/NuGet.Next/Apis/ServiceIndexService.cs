using Gnarly.Data;
using NuGet.Next.Core;
using NuGet.Next.Protocol.Models;

namespace NuGet.Next.Service;

public class ServiceIndexService(IServiceIndexService serviceIndex) : IScopeDependency
{
    // GET v3/index
    public async Task<ServiceIndexResponse> GetAsync(CancellationToken cancellationToken)
    {
        return await serviceIndex.GetAsync(cancellationToken);
    }
}