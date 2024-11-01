namespace NuGet.Next.Protocol.Models;

public class AuthenticationResponse
{
    public string Token { get; set; }
    
    public bool Success { get; set; }
    
    public string Message { get; set; }
}