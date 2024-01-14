using AuthServer.Service.Mappers;
using Microsoft.AspNetCore.Identity;

namespace AuthServer.Service.Services;
public class UserService : IUserService
{
    private readonly UserManager<UserApp> _userManager;

    public UserService(UserManager<UserApp> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ResponseDTO<UserAppDTO>> CreateUSerAsync(CreateUserDTO createUserDTO)
    {
        var user = new UserApp
        {
            City = createUserDTO.City,
            Email = createUserDTO.Email,
            UserName = createUserDTO.UserName,
        };
        var result = await _userManager.CreateAsync(user, createUserDTO.Password);
        if (!result.Succeeded) 
        {
            var errors = result.Errors.Select(x => x.Description).ToList();
            return ResponseDTO<UserAppDTO>.Fail(new ErrorDTO(errors, true), 400);
        }
        return ResponseDTO<UserAppDTO>.Succes(ObjectMapper.Mapper.Map<UserAppDTO>(user), 200);
    }
    public async Task<ResponseDTO<UserAppDTO>> GetUserByNameAsync(string userName)
    {
        var user=await _userManager.FindByNameAsync(userName);
        if (user == null) 
        {
            return ResponseDTO<UserAppDTO>.Fail("UserName Not Found", 404, true);
        }
        return ResponseDTO<UserAppDTO>.Succes(ObjectMapper.Mapper.Map<UserAppDTO>(user), 200);
    }
}