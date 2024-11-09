using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using NuGet.Next.Extensions;
using Thor.Service.Infrastructure.Helper;

namespace NuGet.Next.Core;

public class UserContext(IHttpContextAccessor httpContextAccessor, JwtHelper jwtHelper) : IUserContext
{
    private ClaimsPrincipal User
    {
        get
        {
            // 获取header中的用户信息
            var key = httpContextAccessor.HttpContext?.Request.Headers[HttpContextExtensions.ApiKeyHeader].ToString();

            if (!string.IsNullOrEmpty(key) && !key.StartsWith("key-"))
            {
                return new ClaimsPrincipal(new[] { new ClaimsIdentity(jwtHelper.GetClaimsFromToken(key)) });
            }

            return httpContextAccessor.HttpContext?.User;
        }
    }


    public string UserId
    {
        get
        {
            var userId = User.FindFirst(ClaimTypes.Sid)?.Value;

            return userId ?? string.Empty;
        }
    }

    public bool IsAuthenticated => string.IsNullOrEmpty(UserId) == false;

    public string IpAddress
    {
        get
        {
            var ipAddress = httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();

            if (httpContextAccessor.HttpContext?.Request.Headers.ContainsKey("X-Forwarded-For") == true)
            {
                ipAddress = httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"];
            }

            return ipAddress ?? string.Empty;
        }
    }

    public string Role
    {
        get
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            return role ?? string.Empty;
        }
    }
}