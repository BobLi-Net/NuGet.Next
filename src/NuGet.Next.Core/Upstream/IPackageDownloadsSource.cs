namespace NuGet.Next.Core;

public interface IPackageDownloadsSource
{
    Task<Dictionary<string, Dictionary<string, long>>> GetPackageDownloadsAsync();
}