using System;

namespace MicroserviceENPS.UserServices.Entities
{
    public class DomainValidadorException : Exception
    {
        public DomainValidadorException(string errorMessage) : base(errorMessage)
        {

        }

        public static void Whem(bool errorExists, string errorMessage)
        {
            if (errorExists)
            {
                throw new DomainValidadorException(errorMessage);
            }
        }
    }
}