using AuthServer.Core.Configuration;
using AuthServer.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SharedLibrary.Configrations;
using SharedLibrary.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace AuthServer.Service.Services;
public class TokenService : ITokenService
{
    private readonly UserManager<UserApp> _userManager;
    private readonly CustomTokenOptions _tokenOption;
    public TokenService(UserManager<UserApp> userManager,IOptions<CustomTokenOptions> options)
    {
        _userManager = userManager;
        _tokenOption = options.Value;
    }
    private IEnumerable<Claim> GetClaims(UserApp userApp,List<string> audiences) 
    {
        var userList=new List<Claim> 
        {
         new Claim(ClaimTypes.NameIdentifier,userApp.Id),
         new Claim(JwtRegisteredClaimNames.Email,userApp.Email),
         new Claim(ClaimTypes.Name,userApp.UserName),
         new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
        };
        userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
        return userList;
    }
    private IEnumerable<Claim> GetClaimByClient(Client client) 
    {
    var claims=new List<Claim>();
        claims.AddRange(client.Audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
        new Claim(JwtRegisteredClaimNames.Sub, client.Id.ToString());
        return claims;
    }
    private string CreateRefreshToken() 
    {
    var numberByte=new byte[32];
        using var rnd = RandomNumberGenerator.Create();
        rnd.GetBytes(numberByte);
        return Convert.ToBase64String(numberByte); 
    }
    public TokenDTO CreateToken(UserApp userApp)
    {
        
        var accessTokenexpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
        var refreshTokenexpiration = DateTime.Now.AddMinutes(_tokenOption.RefreshTokenExpiration);
        var securityKey = SignService.GetSymetricSecurityKey(_tokenOption.SecurityKey);
        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
            (
            issuer: _tokenOption.Issuer,
            expires: accessTokenexpiration,
            notBefore: DateTime.Now,
            claims: GetClaims(userApp, _tokenOption.Audience),
            signingCredentials: signingCredentials
            );
        var handler=new JwtSecurityTokenHandler();
        var token=handler.WriteToken(jwtSecurityToken);
        var tokenDTO = new TokenDTO
        {
            AccessToken = token,
            RefreshToken = CreateRefreshToken(),
            AccessTokenExpitaon = accessTokenexpiration,
            RefreshTokenExpration = refreshTokenexpiration
        };
        return tokenDTO;
    }
    public ClientTokenDTO CreateTokenByClient(Client client)
    {
        var accessTokenexpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
        var securityKey = SignService.GetSymetricSecurityKey(_tokenOption.SecurityKey);
        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
            (
            issuer: _tokenOption.Issuer,
            expires: accessTokenexpiration,
        notBefore: DateTime.Now,
            claims:GetClaimByClient(client),
            signingCredentials: signingCredentials
            );
        var handler = new JwtSecurityTokenHandler();
        var token = handler.WriteToken(jwtSecurityToken);
        var tokenDTO = new ClientTokenDTO
        {
            AccessToken = token,
            AccessTokenExpitaon = accessTokenexpiration,
        };
        return tokenDTO;
    }
}