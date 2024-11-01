namespace NuGet.Next.Core;

public interface IModifiable
{
    public DateTime? UpdatedAt { get; set; }

    public string? Modifier { get; set; }
}