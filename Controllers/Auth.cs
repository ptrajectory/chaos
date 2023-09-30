
using chaos.AuthRepository;
using chaos.Dtos.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chaos.Controllers;

[ApiController]
[Route("api/organizations/{organization_id}/apps/{app_id}/authorize")]

public class AuthController: ControllerBase {

    IAuthService _auth_service;

    public AuthController(IAuthService auth){
        this._auth_service = auth;
    }


    /// <summary>
    /// Generate an access token to be used client side
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /api/organizations/{organization_id}/apps/{app_id}/authorize
    ///     {
    ///         "Content-Type": "application/json",
    ///         "X-App-Id": "app id",
    ///         "X-App-Secret": "app secret"
    ///     }
    ///     
    /// </remarks>
    [AppResourcesAccessFilter]
    [HttpGet]
    public GetAccessToken? getAccessTokne(){
        var context = HttpContext;
        var app_id = context.User.FindFirst("owner")?.Value;
        var env = context.User.FindFirst("environment")?.Value;
        Console.WriteLine($"APP ID:: {app_id}");
        if(app_id is null || env is null) {

            return null;

        }
        var token = this._auth_service.generateAccessToken(app_id, env);

        return token;
    }

}