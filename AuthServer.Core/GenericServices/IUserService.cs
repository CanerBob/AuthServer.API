namespace AuthServer.Core.GenericServices;
public interface IUserService
{
    Task<ResponseDTO<UserAppDTO>> CreateUSerAsync(CreateUserDTO createUserDTO);
    Task<ResponseDTO<UserAppDTO>> GetUserByNameAsync(string userName);
}
