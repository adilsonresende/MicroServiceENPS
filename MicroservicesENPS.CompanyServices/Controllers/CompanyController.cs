using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using MicroserviceENPS.Contracts;
using MicroservicesENPS.CompanyServices.DTOs;
using MicroservicesENPS.CompanyServices.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicesENPS.CompanyServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _iCompanyService;
        private readonly IPublishEndpoint _iPublishEndpoint;

        public CompanyController(ICompanyService iCompanyService, IPublishEndpoint iPublishEndpoint)
        {
            _iCompanyService = iCompanyService;
            _iPublishEndpoint = iPublishEndpoint;
        }

        [HttpGet]
        [Route(nameof(GetAllAsync))]
        public async Task<IActionResult> GetAllAsync([FromBody] CompanyFilterDTO companyFilterDTO)
        {
            ServiceResponse<List<CompanyDTO>> serviceResponse = await _iCompanyService.GetAllAsync(companyFilterDTO);

            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse);
            }

            return Ok(serviceResponse);
        }

        [HttpGet]
        [Route(nameof(GetAsync) + "/{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            ServiceResponse<CompanyDTO> serviceResponse = await _iCompanyService.GetAsync(id);

            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse);
            }

            return Ok(serviceResponse);
        }

        [HttpPost]
        [Route(nameof(InsertAsync))]
        public async Task<IActionResult> InsertAsync(CompanyDTO companyDTO)
        {
            ServiceResponse<Guid> serviceResponse = await _iCompanyService.InsertAsync(companyDTO);

            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse);
            }

            await _iPublishEndpoint.Publish(new CompanyCreated(serviceResponse.Data, companyDTO.FantasyName, companyDTO.Name));
            return Ok(serviceResponse);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(CompanyDTO companyDTO)
        {
            ServiceResponse<bool> serviceResponse = await _iCompanyService.UpdateAsync(companyDTO);

            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse);
            }

            await _iPublishEndpoint.Publish(new CompanyUpdated(companyDTO.Id, companyDTO.FantasyName, companyDTO.Name));
            return Ok(serviceResponse);
        }
    }
}