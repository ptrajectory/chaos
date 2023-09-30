

using System.Security.Claims;
using System.Security.Cryptography;
using chaos.AuthRepository;
using chaos.Dtos.Auth;
using chaos.utils;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

public class AuthServive : IAuthService
{
    public GetAccessToken generateAccessToken(string APP_ID, string environment)
    {
        var rsaKey = RSA.Create();
        rsaKey.ImportRSAPrivateKey(AppEnvironment.APP_RSA_KEY_PAIR, out _);

        var key = new RsaSecurityKey(rsaKey);

        var handler = new JsonWebTokenHandler();

        var token = handler.CreateToken(new SecurityTokenDescriptor(){
            Subject = new ClaimsIdentity(new [] {
                new Claim("owner",APP_ID),
                new Claim("environment", environment),
                new Claim("scope", "resources")
            }),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256),
            Expires= DateTime.Now.AddMinutes(60)
        }); 

        return new GetAccessToken(){
            AccessToken = token,
            duration = 3600
        };
    }
}