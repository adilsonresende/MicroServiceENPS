using System.Threading.Tasks;
using MicroserviceENPS.UserServices.DTOs;
using MicroserviceENPS.UserServices.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceENPS.UserServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _iAuthService;
        public AuthController(IAuthService iAuthService)
        {
            _iAuthService = iAuthService;
        }

        [HttpPost]
        [Route(nameof(RegisterAsync))]
        public async Task<IActionResult> RegisterAsync(UserToInsertDTO userToInsertDTO)
        {
            ServiceResponse<UserToInsertDTO> _serviceResponse = await _iAuthService.RegisterAsync(userToInsertDTO);
            if (!_serviceResponse.Success)
            {
                return BadRequest(_serviceResponse);
            }

            return Ok(_serviceResponse);
        }

        [HttpGet]
        [Route(nameof(LoginAsync))]
        public async Task<IActionResult> LoginAsync([FromBody]UserToInsertDTO userToInsertDTO)
        {
            ServiceResponse<string> _serviceResponse = await _iAuthService.LoginAsync(userToInsertDTO);
            if (!_serviceResponse.Success)
            {
                return BadRequest(_serviceResponse);
            }

            return Ok(_serviceResponse);
        }
    }
}