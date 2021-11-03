using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MicroservicesENPS.CompanyServices.DTOs;
using MicroservicesENPS.CompanyServices.Entities;
using MicroservicesENPS.CompanyServices.Repositories.Interfaces;
using MicroservicesENPS.CompanyServices.Services.Interfaces;

namespace MicroservicesENPS.CompanyServices.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IMapper _iMapper;
        private readonly ICompanyRepository _iCompanyReposittory;
        public CompanyService(IMapper iMapper, ICompanyRepository iCompanyRepository)
        {
            _iMapper = iMapper;
            _iCompanyReposittory = iCompanyRepository;
        }
        public async Task<ServiceResponse<List<CompanyDTO>>> GetAllAsync(CompanyFilterDTO companyFilterDTO)
        {
            ServiceResponse<List<CompanyDTO>> serviceResponse = new ServiceResponse<List<CompanyDTO>>();
            try
            {
                List<Company> company = await _iCompanyReposittory.GetAllAsync(companyFilterDTO);
                serviceResponse.Data = _iMapper.Map<List<CompanyDTO>>(company);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<CompanyDTO>> GetAsync(Guid id)
        {
            ServiceResponse<CompanyDTO> serviceResponse = new ServiceResponse<CompanyDTO>();
            try
            {
                Company company = await _iCompanyReposittory.GetAsync(id);
                serviceResponse.Data = _iMapper.Map<CompanyDTO>(company);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Guid>> InsertAsync(CompanyDTO companyDTO)
        {
            ServiceResponse<Guid> serviceResponse = new ServiceResponse<Guid>();
            try
            {
                Company company = new Company(Guid.NewGuid(), companyDTO.IdUser, companyDTO.FantasyName, companyDTO.Name, companyDTO.CNPJ, companyDTO.IE);
                await _iCompanyReposittory.InsertAsync(company);
                serviceResponse.Data = company.Id;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<bool>> UpdateAsync(CompanyDTO companyDTO)
        {
            ServiceResponse<bool> serviceResponse = new ServiceResponse<bool>();
            try
            {
                Company company = new Company(Guid.NewGuid(), companyDTO.IdUser, companyDTO.FantasyName, companyDTO.Name, companyDTO.CNPJ, companyDTO.IE);
                await _iCompanyReposittory.UpdateAsync(company);
                serviceResponse.Data = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}