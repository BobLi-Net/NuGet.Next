using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
using NuGet.Next.Extensions;
using Thor.Service.Infrastructure.Helper;

namespace NuGet.Next.Core;

public class ApiKeyAuthenticationService(IContext context, JwtHelper jwtHelper)
    : IAuthenticationService
{
    public async Task<bool> AuthenticateAsync(HttpContext httpContext)
    {
        var apiKey = httpContext.GetApiKey();

        if (apiKey == null)
            return false;

        if (apiKey.StartsWith("key-"))
        {
            var result = await (from key in context.UserKeys
                join user in context.Users
                    on key.UserId equals user.Id into userGroup
                from user in userGroup.DefaultIfEmpty()
                where key.Key == apiKey && key.Enabled
                select user).FirstOrDefaultAsync();

            if (result == null)
                return false;
            
            // 将用户信息设置到上下文中
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(jwtHelper.GetClaimsFromToken(result)));
            
            return true;
        }
        else
        {
            var (id, role, fullName) = jwtHelper.GetUserFromToken(apiKey);

            if (id == null)
                return false;

            // 判断用户是否存在
            var query = await context.Users.FirstOrDefaultAsync(u => u.Id == id);

            return query != null;
        }
    }

    public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticateInput input)
    {
        var query = await context.Users.Where(u => u.Username == input.Username)
            .FirstOrDefaultAsync();

        if (query == null)
        {
            return new AuthenticationResponse
            {
                Success = false,
                Message = "用户不存在"
            };
        }

        if (!query.VerifyPassword(input.Password))
            return new AuthenticationResponse
            {
                Success = false,
                Message = "密码错误"
            };

        var token = jwtHelper.CreateToken(query);

        return new AuthenticationResponse
        {
            Success = true,
            Token = token
        };
    }
}