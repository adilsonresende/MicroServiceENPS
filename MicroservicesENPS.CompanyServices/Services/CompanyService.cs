using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MicroservicesENPS.CompanyServices.DTOs;
using MicroservicesENPS.CompanyServices.Services.Interfaces;

namespace MicroservicesENPS.CompanyServices.Services
{
    public class CompanyService : ICompanyService
    {
        public Task<ServiceResponse<List<CompanyDTO>>> GetAllAsync(CompanyFilterDTO companyFilterDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<CompanyDTO>> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<Guid>> InsertAsync(CompanyDTO companyDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<bool>> UpdateAsync(CompanyDTO companyDTO)
        {
            throw new NotImplementedException();
        }
    }
}