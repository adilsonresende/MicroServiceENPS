using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MicroserviceENPS.UserServices.DTOs;
using MicroserviceENPS.UserServices.Entities;
using MicroserviceENPS.UserServices.Repositories.Interfaces;
using MicroserviceENPS.UserServices.Services.Interfaces;

namespace MicroserviceENPS.UserServices.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _iMapper;
        private readonly IUserRepository _iUserRepository;

        public UserService(IMapper iMapper, IUserRepository iUserRepository)
        {
            _iMapper = iMapper;
            _iUserRepository = iUserRepository;
        }

        public async Task<ServiceResponse<List<UserDTO>>> GetAll(UserFilter userFilter)
        {
            await Task.Delay(1);
            return new ServiceResponse<List<UserDTO>>();
        }


        public async Task<ServiceResponse<bool>> LogicalDeleteAsync(UserDTO userDTO)
        {
            ServiceResponse<bool> serviceResponse = new ServiceResponse<bool>();
            try
            {
                await _iUserRepository.LogicalDeleteAsync(userDTO.Id);
                serviceResponse.Data = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.GetBaseException().Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<bool>> UpdateAsync(UserDTO userDTO)
        {
            ServiceResponse<bool> serviceResponse = new ServiceResponse<bool>();
            try
            {
                User user = _iMapper.Map<User>(userDTO);
                await _iUserRepository.UpdateAsync(user);
                serviceResponse.Data = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.GetBaseException().Message;
            }

            return serviceResponse;
        }
    }
}