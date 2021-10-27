using System;

namespace MicroservicesENPS.CompanyServices.DTOs
{
    public class CompanyDTO
    {
        public Guid Id { get; private set; }
        public Guid IdUser { get; private set; }
        public bool IsActive { get; set; } = true;
        public string FantasyName { get; private set; }
        public string Name { get; private set; }
        public string CNPJ { get; private set; }
        public string IE { get; private set; }
    }
}