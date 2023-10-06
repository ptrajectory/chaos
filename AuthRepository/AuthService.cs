

using System.Security.Claims;
using System.Security.Cryptography;
using chaos.AuthRepository;
using chaos.Dtos.Auth;
using chaos.Models;
using chaos.utils;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

public class AuthServive : IAuthService
{

    private readonly ChatContext _context;

    public AuthServive(ChatContext context ){
        this._context = context;
    }

    public GetAccessToken generateAccessToken(string APP_ID, string environment, string user_id)
    {

        var user = _context.USER.FirstOrDefault((u)=>u.AppID == APP_ID && u.ID == user_id) ?? throw new Exception("User doesnt belong to the specified app");


        var rsaKey = RSA.Create();
        rsaKey.ImportRSAPrivateKey(AppEnvironment.APP_RSA_KEY_PAIR, out _);

        var key = new RsaSecurityKey(rsaKey);

        var handler = new JsonWebTokenHandler();

        var token = handler.CreateToken(new SecurityTokenDescriptor(){
            Subject = new ClaimsIdentity(new [] {
                new Claim("owner",APP_ID),
                new Claim("environment", environment),
                new Claim("scope", "resources"),
                new Claim("user", user_id),
                new Claim("expires", DateTime.UtcNow.AddMinutes(60).ToString())
            }),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256),
            Expires= DateTime.UtcNow.AddMinutes(60)
        }); 

        return new GetAccessToken(){
            AccessToken = token,
            duration = 3600
        };
    }
}