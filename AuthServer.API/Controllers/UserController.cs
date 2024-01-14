using AuthServer.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDTO createUserDTO) 
        {
            return ActionResltInstance(await _userService.CreateUSerAsync(createUserDTO));
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUSer() 
        {
            return ActionResltInstance(await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name));
        }
    }
}