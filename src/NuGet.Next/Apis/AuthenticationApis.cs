using Gnarly.Data;
using NuGet.Next.Core;
using NuGet.Next.Protocol.Models;

namespace NuGet.Next.Service;

public class AuthenticationApis(IAuthenticationService authenticationService) : IScopeDependency
{
    public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticateInput input)
    {
        var authenticated = await authenticationService.AuthenticateAsync(input);
        
        return authenticated;
    }
}