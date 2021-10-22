using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using MicroserviceENPS.UserServices.DTOs;
using MicroserviceENPS.UserServices.Entities;
using MicroserviceENPS.UserServices.Helpers;
using MicroserviceENPS.UserServices.Repositories.Interfaces;
using MicroserviceENPS.UserServices.Services.Interfaces;

namespace MicroserviceENPS.UserServices.Services
{
    public class AuthService: IAuthService
    {
        private readonly IConfiguration _iConfiguration;
        private readonly IUserRepository _iUserRepository;

        public AuthService(IUserRepository iUserRepository, IConfiguration iConfiguration)
        {
            _iUserRepository = iUserRepository;
            _iConfiguration = iConfiguration;
        }
        public async Task<ServiceResponse<string>> LoginAsync(UserToInsertDTO userToInsertDTO)
        {
            ServiceResponse<string> serviceResponse = new ServiceResponse<string>();

            try
            {
                User user = await _iUserRepository.GetByEmailAsync(userToInsertDTO.Email);
                if (user is null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = Messages.PtBrMessages.UserNotFound;
                    return serviceResponse;
                }
                else if (!PasswordHelper.VerifyPasswordHash(userToInsertDTO.Password, user.PasswordHash, user.PasswordSalt))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = Messages.PtBrMessages.UserInvalid;
                    return serviceResponse;
                }

                serviceResponse.Data = CreateTokem(user);
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.GetBaseException().Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<UserToInsertDTO>> RegisterAsync(UserToInsertDTO userToInsertDTO)
        {
            ServiceResponse<UserToInsertDTO> serviceResponse = new ServiceResponse<UserToInsertDTO>();
            try
            {
                if (await UserExists(userToInsertDTO))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = Messages.PtBrMessages.UserAlreadExists;
                    return serviceResponse;
                }

                PasswordHelper.CreatePasswordHash(userToInsertDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
                User user = new User(Guid.NewGuid(), userToInsertDTO.Name, userToInsertDTO.Login, userToInsertDTO.Email, Enums.UserRole.User, passwordHash, passwordSalt);
                await _iUserRepository.InsertAsync(user);

                serviceResponse.Data = userToInsertDTO;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.GetBaseException().Message;
                serviceResponse.Success = false;
                return serviceResponse;
            }
        }

        private async Task<bool> UserExists(UserToInsertDTO userToInsertDTO)
        {
            User user = await _iUserRepository.GetByEmailAsync(userToInsertDTO.Email);
            return user != null;
        }

        private string CreateTokem(User user)
        {
            List<Claim> claim = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
            };

            SymmetricSecurityKey systemSecurityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_iConfiguration.GetSection("AppSettings:Token").Value)
            );

            SigningCredentials signingCredentials = new SigningCredentials(systemSecurityKey, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = signingCredentials
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(securityToken);
        }
    }
}