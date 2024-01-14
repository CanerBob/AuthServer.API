using Azure;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DTOs;

namespace AuthServer.API.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult ActionResltInstance<T>(ResponseDTO<T> response) where T : class
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
