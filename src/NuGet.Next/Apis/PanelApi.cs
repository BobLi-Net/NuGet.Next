using Gnarly.Data;
using Microsoft.EntityFrameworkCore;
using NuGet.Next.Core;
using NuGet.Next.Protocol.Models;

namespace NuGet.Next.Service;

public class PanelApi(IContext context): IScopeDependency
{
    public async Task<PanelResponse> GetAsync()
    {
        var users =await context.Users.LongCountAsync();
        
        var packages = await context.Packages.LongCountAsync();
        
        // 本周新增包数量
        var weekPackages = await context.Packages
            .Where(x => x.CreatedAt >= DateTime.Now.AddDays(-7))
            .LongCountAsync();
        
        // 下载总量
        var downloadCount = await context.Packages.SumAsync(x=>x.Downloads);
        
        return new PanelResponse
        {
            UserCount = users,
            PackageCount = packages,
            NewPackageCount = weekPackages,
            DownloadCount = downloadCount
        };
    }
}