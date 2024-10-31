using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace NuGet.Next.Core;

public class UriToStringConverter : ValueConverter<Uri, string>
{
    public static readonly UriToStringConverter Instance = new();

    public UriToStringConverter()
        : base(
            v => v.AbsoluteUri,
            v => new Uri(v))
    {
    }
}