namespace MicroservicesENPS.CompanyServices.DTOs
{
    public class CompanyFilterDTO
    {
        public int PageSize { get; set; } = 100;
        public int PageNumber = 1;
        public bool? IsActive { get; set; }
        public string FantasyName { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public string IE { get; private set; }
    }
}