using System.ComponentModel.DataAnnotations;

namespace NuGet.Next.Options;

public class DatabaseOptions
{
    public string Type { get; set; }

    [Required] public string ConnectionString { get; set; }
}