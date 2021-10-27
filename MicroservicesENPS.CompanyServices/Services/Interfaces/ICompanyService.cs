using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MicroservicesENPS.CompanyServices.DTOs;

namespace MicroservicesENPS.CompanyServices.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<ServiceResponse<List<CompanyDTO>>> GetAllAsync(CompanyFilterDTO companyFilterDTO);
        Task<ServiceResponse<CompanyDTO>> GetAsync(Guid id);
        Task<ServiceResponse<Guid>> InsertAsync(CompanyDTO companyDTO);
        Task<ServiceResponse<bool>> UpdateAsync(CompanyDTO companyDTO);
    }
}