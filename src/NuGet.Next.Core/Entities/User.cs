namespace NuGet.Next.Core;

public class User
{
    public string Id { get; set; }

    public string FullName { get; set; }
    
    public string Username { get; set; }

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }

    public string Password { get; set; }

    public string? Avatar { get; set; }

    public string Role { get; set; }
}