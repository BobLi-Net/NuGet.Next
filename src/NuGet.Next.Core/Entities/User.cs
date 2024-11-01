using Thor.Service.Infrastructure.Helper;

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

    public void SetPassword(string password)
    {
        PasswordHash = Guid.NewGuid().ToString("N");
        Password = StringHelper.HashPassword(password, PasswordHash);
    }

    public bool VerifyPassword(string password)
    {
        return Password == StringHelper.HashPassword(password, PasswordHash);
    }
}