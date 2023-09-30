using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace chaos.AuthRepository;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RequiresClaimAttribute : Attribute, IAuthorizationFilter
{

    private readonly string _claimName;
    private readonly string? _claimValue;

    private readonly string? _param;

    public RequiresClaimAttribute(string claimName, string claimValue)
    {
        this._claimName = claimName;
        this._claimValue = claimValue;
    }

    public RequiresClaimAttribute(string claimName,  string param, string? claimValue)
    {
        this._claimName = claimName; 
        this._claimValue = claimValue;
        this._param = param;
    }
    
    void IAuthorizationFilter.OnAuthorization(AuthorizationFilterContext context)
    {   
        if(_claimValue is null) return;
        if(!context.HttpContext.User.HasClaim(_claimName, _claimValue)){
            if(_param is null){
                context.HttpContext.User.HasClaim(IdentityData.FriendClaimName, "true");
                context.Result = new ForbidResult();
                return;
            }
        }

        if(_param is not null)
        {
            var param = context.HttpContext.Request.RouteValues[_param]?.ToString();

            if(param is null){
                context.Result = new ForbidResult();
                return;
            }
            
            if (!context.HttpContext.User.HasClaim(_claimName, param))
            {
                context.Result = new ForbidResult();
                return;
            } 

        }
    }
}