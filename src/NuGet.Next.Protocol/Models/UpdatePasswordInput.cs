namespace NuGet.Next.Protocol.Models;

public class UpdatePasswordInput
{
    public string Password { get; set; }
    
    public string OldPassword { get; set; }
}