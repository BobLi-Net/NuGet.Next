namespace NuGet.Next.Protocol.Models;

public class OkResponse
{
    public bool Success { get; set; }
    
    public string Message { get; set; }
    
    public OkResponse(bool success, string message)
    {
        Success = success;
        Message = message;
    }
    
    public static OkResponse Ok(string message)
    {
        return new OkResponse(true, message);
    }
}