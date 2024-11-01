namespace NuGet.Next.Core;

public class TargetFramework
{
    public int Key { get; set; }

    public string? Moniker { get; set; }

    public string PackageId { get; set; }
    
    public Package Package { get; set; }
}