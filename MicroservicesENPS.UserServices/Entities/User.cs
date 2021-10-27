using System;
using MicroserviceENPS.UserServices.Enums;

namespace MicroserviceENPS.UserServices.Entities
{
    public class User
    {
        public User()
        {

        }

        private string IdIsNotValid = "IdUser inválido!";
        private string NameIsNotValid = "Name inválido!";
        private string LoginIsNotValid = "Login inválido!";
        private string EmailIsNotValid = "Email inválido!";
        private string UserRoleNotValid = "User Role inválido!";
        private string PasswordHashIsNotValid = "PasswordSalt hash inválido!";
        private string PasswordSaltIsNotValid = "PasswordHash salt inválido!";
        public User(Guid Id, string name, string login, string email, UserRole userRole, byte[] passwordHash, byte[] passwordSalt)
        {
            DomainValidadorException.Whem(Id == Guid.Empty, IdIsNotValid);
            this.Id = Id;

            DomainValidadorException.Whem(string.IsNullOrWhiteSpace(name), NameIsNotValid);
            Name = name;

            DomainValidadorException.Whem(string.IsNullOrWhiteSpace(login), LoginIsNotValid);
            Login = login;

            bool isValidEmail = !string.IsNullOrWhiteSpace(email) && IsValidEmail(email);
            DomainValidadorException.Whem(!isValidEmail, EmailIsNotValid);
            Email = email;

            DomainValidadorException.Whem(!Enum.IsDefined(userRole), UserRoleNotValid);
            UserRole = userRole;

            DomainValidadorException.Whem(passwordHash == null || passwordHash.Length == 0, PasswordSaltIsNotValid);
            PasswordHash = passwordHash;

            DomainValidadorException.Whem(passwordSalt == null || passwordSalt.Length == 0, PasswordHashIsNotValid);
            PasswordSalt = passwordSalt;
        }

        public Guid Id { get; private set; }
        public bool IsActive { get; private set; } = true;
        public string Name { get; private set; }
        public string Login { get; private set; }
        public string Email { get; private set; }
        public UserRole UserRole { get; private set; }
        public byte[] PasswordSalt { get; private set; }
        public byte[] PasswordHash { get; private set; }

        public void ChangeIsActiveState()
        {
            IsActive = !IsActive;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}