﻿using AuthServer.Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : CustomBaseController
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateToken(LoginDTO loginDTO) 
        {
        var result= await _authenticationService.CreateTokenAsync(loginDTO);
            return ActionResltInstance(result);
        }
        [HttpPost]
        public IActionResult CreateTokenByClient(ClientLoginDTO clientLoginDTO) 
        {
        var result=_authenticationService.CreateTokenByClient(clientLoginDTO);
            return ActionResltInstance(result);
        }
        [HttpPost]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDTO refreshTokenDTO) 
        {
        var result= await _authenticationService.RevokeRefreshToken(refreshTokenDTO.Token);
            return ActionResltInstance(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenDTO refreshTokenDTO) 
        {
            var result = await _authenticationService.CreateTokenByRefreshToken(refreshTokenDTO.Token);
            return ActionResltInstance(result); 
        }
    }
}
