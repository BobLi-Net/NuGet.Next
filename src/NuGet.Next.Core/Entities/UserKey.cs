namespace NuGet.Next.Core;

public class UserKey 
{
    public long Id { get; set; }

    public string Key { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTimeOffset CreatedTime { get; set; }

    /// <summary>
    /// 用户id
    /// </summary>
    public string UserId { get; set; }
    
    /// <summary>
    /// 创建人
    /// </summary>
    public User User { get; set; }
}