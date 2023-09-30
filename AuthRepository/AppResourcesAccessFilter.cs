

using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using chaos.utils;
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

        Console.WriteLine($"GOT THE APP ID:: {APP_ID}");

        context.HttpContext.Request.Headers.TryGetValue("x-app-secret", out var APP_SECRET);

        Console.WriteLine($"GOT THE APP SECRET:: {APP_SECRET}");

        if(APP_SECRET.IsNullOrEmpty())
        {
            context.Result = new UnauthorizedObjectResult("NO APP SECRET PROVIDED");
            return;
        }

        var token = utils.Utils.GetDecryptedJwt(APP_SECRET.ToString());

        Console.WriteLine(token);

        if(token is null)
        {
            context.Result = new UnauthorizedObjectResult("INVALID TOKEN PROVIDED");
            return;
        }

        // context.HttpContext.Request.Headers.Add("Authorization", $"Bearer {token}");

        var rsa = RSA.Create();
        rsa.ImportRSAPrivateKey(AppEnvironment.APP_RSA_KEY_PAIR, out _); 


        var tokenHandler = new JwtSecurityTokenHandler();

        var validationParameters = new TokenValidationParameters()
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = false,
            RequireExpirationTime = false,
            IssuerSigningKey = new RsaSecurityKey(rsa),
            ValidateIssuerSigningKey = true
        };


        try {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out _); 

            if(!principal.HasClaim(c => c.Type == "owner" && c.Value == APP_ID))
            {
                context.Result = new ForbidResult();
                return;
            }
            context.HttpContext.User = principal;
            Console.WriteLine("We are good to go");
        }
        catch (SecurityTokenException)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

    }
}