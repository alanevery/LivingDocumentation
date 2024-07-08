using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AW.Test.Utilities.Builders;

public class AuthorizationHandlerContextBuilder
{    
    private ClaimsPrincipal? _principal;
    private List<IAuthorizationRequirement> requirements = new List<IAuthorizationRequirement>();

    public AuthorizationHandlerContextBuilder WithPrincipal(ClaimsPrincipal principal)
    {
        _principal = principal;

        return this;
    }     

    public AuthorizationHandlerContextBuilder AddRequirement(IAuthorizationRequirement requirement)
    {
        requirements.Add(requirement);

        return this;
    }
       
    public AuthorizationHandlerContext Build() => new AuthorizationHandlerContext(requirements, _principal, null);   
}
