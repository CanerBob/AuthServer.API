namespace AuthServer.Core.GenericServices;
public interface ITokenService
{
    TokenDTO CreateToken(UserApp userApp);
    ClientTokenDTO CreateTokenByClient(Client client);
}
