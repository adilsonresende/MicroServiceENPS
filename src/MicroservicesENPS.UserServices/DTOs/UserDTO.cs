using System;
using MicroserviceENPS.UserServices.Enums;

namespace MicroserviceENPS.UserServices.DTOs
{
    public class UserDTO : UserToInsertDTO
    {
        public Guid Id { get; set; }
        public bool IsEmailconfirmed { get; set; }
        public UserRole UserRole { get; set; }
    }
}