using System;

namespace MicroservicesENPS.CompanyServices.DTOs
{
    public class CompanyDTO
    {
        public Guid Id { get; set; }
        public Guid IdUser { get; set; }
        public bool IsActive { get; set; } = true;
        public string FantasyName { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public string IE { get; set; }
    }
}