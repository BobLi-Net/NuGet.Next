namespace NuGet.Next.Protocol.Models;

public class UpdatePasswordInput
{
    public string NewPassword { get; set; }
    
    public string CurrentPassword { get; set; }
}