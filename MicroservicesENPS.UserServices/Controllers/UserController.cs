using System.Collections.Generic;
using System.Threading.Tasks;
using MicroserviceENPS.UserServices.DTOs;
using MicroserviceENPS.UserServices.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MicroserviceENPS.UserServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _iUserService;

        public UserController(IUserService iUserService)
        {
            _iUserService = iUserService;
        }

        [HttpGet]
        [Route(nameof(GetAll))]
        public async Task<IActionResult> GetAll([FromBody] UserFilter userFilter)
        {
            ServiceResponse<List<UserDTO>> _serviceResponse = await _iUserService.GetAll(userFilter);
            if (!_serviceResponse.Success)
            {
                return BadRequest(_serviceResponse);
            }

            return Ok(_serviceResponse.Message);
        }

        [HttpPut]
        [Route(nameof(UpdateAsync))]
        public async Task<IActionResult> UpdateAsync([FromBody] UserDTO userDTO)
        {
            ServiceResponse<bool> _serviceResponse = await _iUserService.UpdateAsync(userDTO);
            if (!_serviceResponse.Success)
            {
                return BadRequest(_serviceResponse);
            }

            return Ok(_serviceResponse.Message);
        }

        [HttpDelete]
        [Route(nameof(LogicalDeleteAsync))]
        public async Task<IActionResult> LogicalDeleteAsync([FromBody] UserDTO userDTO)
        {
            ServiceResponse<bool> _serviceResponse = await _iUserService.LogicalDeleteAsync(userDTO);
            if (!_serviceResponse.Success)
            {
                return BadRequest(_serviceResponse);
            }

            return Ok(_serviceResponse.Message);
        }
    }
}