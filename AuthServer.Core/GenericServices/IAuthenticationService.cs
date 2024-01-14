namespace AuthServer.Core.GenericServices;
public interface IAuthenticationService
{
    Task<ResponseDTO<TokenDTO>> CreateTokenAsync(LoginDTO loginDTO);
    Task<ResponseDTO<TokenDTO>> CreateTokenByRefreshToken(string refreshToken);
    Task<ResponseDTO<NoDataDTO>> RevokeRefreshToken(string refreshToken);
    ResponseDTO<ClientTokenDTO> CreateTokenByClient(ClientLoginDTO clientLoginDTO);
}