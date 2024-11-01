namespace NuGet.Next.Core;

public interface IUserContext
{
    string UserId { get; }
    
    bool IsAuthenticated { get; }
    
    string IpAddress { get; }
    
    string Role { get; }
}