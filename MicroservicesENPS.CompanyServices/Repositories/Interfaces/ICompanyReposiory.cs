using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MicroservicesENPS.CompanyServices.DTOs;
using MicroservicesENPS.CompanyServices.Entities;

namespace MicroservicesENPS.CompanyServices.Repositories.Interfaces
{
    public interface ICompanyReposiory
    {
         Task<Guid> InsertAsync(Company Company);
         Task UpdateAsync(Company company);
         Task LogicalDeleteAsync(Guid id);
         Task<Company> GetAsync(Guid id);
         Task<List<Company>> GetAllAsync(CompanyFilterDTO companyFilter);
    }
}