namespace NuGet.Next.Core;

public interface ICreatable
{
    public DateTime CreatedAt { get; set; }

    public string? Creator { get; set; }
}