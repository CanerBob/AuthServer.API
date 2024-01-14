using AuthServer.Core.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AuthServer.Service.Services;
public class AuthenticationService : IAuthenticationService
{
    private readonly List<Client> _clients;
    private readonly ITokenService _tokenService;
    private readonly UserManager<UserApp> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenService;
    public AuthenticationService(IOptions<List<Client>> optionsClient,ITokenService tokenService,UserManager<UserApp> userManager,IUnitOfWork unitOfWork,IGenericRepository<UserRefreshToken> userRefreshTokenService)
    {
        _clients = optionsClient.Value;
        _tokenService = tokenService;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _userRefreshTokenService = userRefreshTokenService;
    }
    public async Task<ResponseDTO<TokenDTO>> CreateTokenAsync(LoginDTO loginDTO)
    {
        if (loginDTO==null) throw new ArgumentNullException(nameof(loginDTO));
        var user=await _userManager.FindByEmailAsync(loginDTO.Email);
        if (user == null) return ResponseDTO<TokenDTO>.Fail("Email or password is wrong",400,true);
        var login = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
        if (!login) 
        {
            return ResponseDTO<TokenDTO>.Fail("Email or password is wrong", 400, true);
        }
        var token = _tokenService.CreateToken(user);
        var userRefreshToken = await _userRefreshTokenService.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();
        if (userRefreshToken == null)
        {
            await _userRefreshTokenService.AddAsync(new UserRefreshToken { UserId = user.Id, Code = token.RefreshToken, Expiraton = token.RefreshTokenExpration });
        }
        else 
        {
            userRefreshToken.Code = token.RefreshToken;
            userRefreshToken.Expiraton = token.RefreshTokenExpration;
        }
        await _unitOfWork.SaveChangesAsync();
        return ResponseDTO<TokenDTO>.Succes(token,200);
    }

    public ResponseDTO<ClientTokenDTO> CreateTokenByClient(ClientLoginDTO clientLoginDTO)
    {
        var client = _clients.SingleOrDefault(x => x.Id == clientLoginDTO.ClientID && x.Secret == clientLoginDTO.ClientSecret);
        if (client == null) 
        {
            return ResponseDTO<ClientTokenDTO>.Fail("ClientId or ClientSecret Not Found", 404, true);
        }
        var token=_tokenService.CreateTokenByClient(client);
        return ResponseDTO<ClientTokenDTO>.Succes(token, 200); 
    }
    public async Task<ResponseDTO<TokenDTO>> CreateTokenByRefreshToken(string refreshToken)
    {
        var exisistRefreshToken = await _userRefreshTokenService.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();
        if (exisistRefreshToken == null) 
        {
            return ResponseDTO<TokenDTO>.Fail("Refresh Token Not Found", 404, true);
        }
        var user = await _userManager.FindByIdAsync(exisistRefreshToken.UserId);
        if (user == null) 
        {
            return ResponseDTO<TokenDTO>.Fail("UserID Not Found", 404, true);
        }
        var tokenDto = _tokenService.CreateToken(user);
        exisistRefreshToken.Code = tokenDto.RefreshToken;
        exisistRefreshToken.Expiraton = tokenDto.RefreshTokenExpration;
        await _unitOfWork.SaveChangesAsync();
        return ResponseDTO<TokenDTO>.Succes(tokenDto, 200);
    }
    public async Task<ResponseDTO<NoDataDTO>> RevokeRefreshToken(string refreshToken)
    {
        var existRefreshToken = await _userRefreshTokenService.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();
        if (existRefreshToken == null) 
        {
            return ResponseDTO<NoDataDTO>.Fail("Refresh Token Not Found", 404, true);
        }
        _userRefreshTokenService.Remove(existRefreshToken);
        await _unitOfWork.SaveChangesAsync();
        return ResponseDTO<NoDataDTO>.Succes(200);
    }
}