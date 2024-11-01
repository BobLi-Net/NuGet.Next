namespace NuGet.Next.Protocol.Models;

public class PackageUpdateRecordResponse
{
    public long Id { get; set; }
    
    /// <summary>
    /// 包id
    /// </summary>
    public string PackageId { get; set; }
    
    /// <summary>
    /// 包版本
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    /// 操作类型
    /// </summary>
    public string OperationType { get; set; }

    /// <summary>
    /// 操作描述
    /// </summary>
    public string OperationDescription { get; set; }

    /// <summary>
    /// 操作IP
    /// </summary>
    public string OperationIP { get; set; }

    /// <summary>
    /// 操作人
    /// </summary>
    public string UserId { get; set; }

    public UserResponse User { get; set; }
    
    /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime OperationTime { get; set; }
}