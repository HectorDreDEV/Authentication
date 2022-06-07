using Microsoft.AspNetCore.Authorization;

namespace Authentication.AuthorizationRequirements;

public class CustomRequireClaim : IAuthorizationRequirement
{
    public string ClaimType { get; }
    public CustomRequireClaim(string claimType)
    {
        ClaimType = claimType;
    }
}

public class CustomRequireClaimHandler : AuthorizationHandler<CustomRequireClaim>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRequireClaim requirement)
    {
        var hasClaim = context.User.Claims.Any((x => x.Type == requirement.ClaimType));
        if (hasClaim)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}