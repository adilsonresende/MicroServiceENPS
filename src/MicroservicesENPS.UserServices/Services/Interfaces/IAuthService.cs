using System.Threading.Tasks;
using MicroserviceENPS.UserServices.DTOs;

namespace MicroserviceENPS.UserServices.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResponse<UserToInsertDTO>> RegisterAsync(UserToInsertDTO userToInsertDTO);
        Task<ServiceResponse<string>> LoginAsync(UserToInsertDTO userToInsertDTO);
    }
}