namespace NuGet.Next.Protocol.Models;

public class PageResponse<T>
{
    public long Total { get; set; }

    public IReadOnlyList<T> Items { get; set; }
    
    public PageResponse(long total, IReadOnlyList<T> items)
    {
        Total = total;
        Items = items;
    }
}