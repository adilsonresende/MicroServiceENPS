using MicroserviceENPS.UserServices.Enums;

namespace MicroserviceENPS.UserServices.DTOs
{
    public class UserFilter
    {
        public int PageSize { get; set; } = 100;
        public int PageNumber = 1;
        public string Name { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public bool? IsEmailconfirmed { get; set; }
        public UserRole UserRole { get; set; }
    }
}