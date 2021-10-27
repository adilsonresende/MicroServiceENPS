using System.Collections.Generic;
using System.Threading.Tasks;
using MicroserviceENPS.UserServices.DTOs;

namespace MicroserviceENPS.UserServices.Services.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<List<UserDTO>>> GetAll(UserFilter userFilter);
        Task<ServiceResponse<bool>> UpdateAsync(UserDTO userDTO);
        Task<ServiceResponse<bool>> LogicalDeleteAsync(UserDTO userDTO);
    }
}