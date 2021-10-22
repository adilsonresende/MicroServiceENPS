using System.Security.Cryptography;
using System.Text;

namespace MicroserviceENPS.UserServices.Helpers
{
   public static class PasswordHelper
    {
        public static void CreatePasswordHash(string senha, out byte[] senhadHash, out byte[] senhaSalt)
        {
            using (var hmacsha = new HMACSHA512())
            {
                senhaSalt = hmacsha.Key;
                senhadHash = hmacsha.ComputeHash(Encoding.UTF8.GetBytes(senha));
            }
        }

        public static bool VerifyPasswordHash(string senha, byte[] senhaHash, byte[] senhadSalt)
        {
            using (var hmacsha = new HMACSHA512(senhadSalt))
            {
                var ComputeHash = hmacsha.ComputeHash(Encoding.UTF8.GetBytes(senha));
                for (int i = 0; i < ComputeHash.Length; i++)
                {
                    if (ComputeHash[i] != senhaHash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}