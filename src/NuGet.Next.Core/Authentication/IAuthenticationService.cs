using Microsoft.AspNetCore.Http;

namespace NuGet.Next.Core;

public interface IAuthenticationService
{
    Task<bool> AuthenticateAsync(HttpContext context);

    /// <summary>
    /// 登录验证
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<AuthenticationResponse> AuthenticateAsync(AuthenticateInput input);
}