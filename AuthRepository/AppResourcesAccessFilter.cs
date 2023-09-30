

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace chaos.AuthRepository;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AppResourcesAccessFilter : Attribute, IAuthorizationFilter
{
    void IAuthorizationFilter.OnAuthorization(AuthorizationFilterContext context)
    {
        context.HttpContext.Request.Headers.TryGetValue("x-app-id", out var APP_ID);

        if(APP_ID.IsNullOrEmpty()){
            // dont extract the authorization header just continue
            return;
        }

        context.HttpContext.Request.Headers.TryGetValue("x-app-secret", out var APP_SECRET);

        if(APP_SECRET.IsNullOrEmpty())
        {
            context.Result = new UnauthorizedObjectResult("NO APP SECRET PROVIDED");
            return;
        }

        var token = utils.Utils.DecryptFromStorage(APP_SECRET.ToString());

        if(token is null)
        {
            context.Result = new UnauthorizedObjectResult("INVALID TOKEN PROVIDED");
            return;
        }

        context.HttpContext.Request.Headers.Add("Authorization", $"Bearer {token}");

    }
}