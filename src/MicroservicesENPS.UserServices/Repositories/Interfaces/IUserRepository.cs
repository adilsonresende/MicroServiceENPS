using System;
using System.Threading.Tasks;
using MicroserviceENPS.UserServices.Entities;

namespace MicroserviceENPS.UserServices.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task<Guid> InsertAsync(User user);
        Task UpdateAsync(User user);
        Task LogicalDeleteAsync(Guid idUser);
    }
}