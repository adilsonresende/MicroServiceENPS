using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MicroservicesENPS.CompanyServices.DTOs;
using MicroservicesENPS.CompanyServices.Entities;
using MongoDB.Driver;

namespace MicroservicesENPS.CompanyServices.Repositories.Interfaces
{
    public class CompanyRepository : ICompanyRepository
    {

        private const string collectionName = nameof(Company);
        private readonly IMongoCollection<Company> _iMongoCollection;
        public readonly FilterDefinitionBuilder<Company> filterDefinitionBuilder = Builders<Company>.Filter;

        public CompanyRepository(IMongoDatabase iMongoDatabase)
        {
            _iMongoCollection = iMongoDatabase.GetCollection<Company>(collectionName);
        }

        public async Task<List<Company>> GetAllAsync(CompanyFilterDTO companyFilter)
        {
            FilterDefinition<Company> filterDefinition = filterDefinitionBuilder.Empty;
            if (companyFilter.IsActive != null)
            {
                FilterDefinition<Company> isActiveFilter = filterDefinitionBuilder.Eq(x => x.IsActive, companyFilter.IsActive);
                filterDefinition &= isActiveFilter;
            }

            if (!string.IsNullOrWhiteSpace(companyFilter.FantasyName))
            {
                FilterDefinition<Company> fantasyNameFilter = filterDefinitionBuilder.AnyIn(x => x.FantasyName, companyFilter.FantasyName);
                filterDefinition &= fantasyNameFilter;
            }

            if (!string.IsNullOrWhiteSpace(companyFilter.FantasyName))
            {
                FilterDefinition<Company> nameFilter = filterDefinitionBuilder.AnyIn(x => x.Name, companyFilter.Name);
                filterDefinition &= nameFilter;
            }

            if (!string.IsNullOrWhiteSpace(companyFilter.CNPJ))
            {
                FilterDefinition<Company> cNPJFilter = filterDefinitionBuilder.AnyIn(x => x.CNPJ, companyFilter.CNPJ);
                filterDefinition &= cNPJFilter;
            }

            if(!string.IsNullOrWhiteSpace(companyFilter.IE)){
                FilterDefinition<Company> IEFilter = filterDefinitionBuilder.AnyIn(x => x.IE, companyFilter.IE);
                filterDefinition &= IEFilter;
            }

            List<Company> company = await _iMongoCollection.Find(filterDefinition).ToListAsync();
            return company;
        }

        public async Task<Company> GetAsync(Guid id)
        {
            FilterDefinition<Company> filterDefinition = filterDefinitionBuilder.Eq(x => x.Id, id);
            return await _iMongoCollection.Find(filterDefinition).FirstOrDefaultAsync();
        }

        public async Task<Guid> InsertAsync(Company company)
        {
            await _iMongoCollection.InsertOneAsync(company);
            return company.Id;
        }

        public async Task LogicalDeleteAsync(Guid id)
        {
            FilterDefinition<Company> filterDefinition = filterDefinitionBuilder.Eq(x => x.Id, id);
            Company company = await _iMongoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            company.ChangeIsActiveState(false);
            await _iMongoCollection.ReplaceOneAsync(filterDefinition, company);
        }

        public async Task UpdateAsync(Company company)
        {
            FilterDefinition<Company> filterDefinition = filterDefinitionBuilder.Eq(x => x.Id, company.Id);
            await _iMongoCollection.ReplaceOneAsync(filterDefinition, company);
        }
    }
}