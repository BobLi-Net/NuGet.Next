namespace NuGet.Next.Protocol.Models;

public class PanelResponse
{
    /// <summary>
    /// 包数量
    /// </summary>
    public long PackageCount { get; set; }
    
    /// <summary>
    /// 本周新增包数量
    /// </summary>
    public long NewPackageCount { get; set; }
    
    /// <summary>
    /// 下载量
    /// </summary>
    public long DownloadCount { get; set; }
    
    /// <summary>
    /// 用户数量
    /// </summary>
    public long UserCount { get; set; }
}