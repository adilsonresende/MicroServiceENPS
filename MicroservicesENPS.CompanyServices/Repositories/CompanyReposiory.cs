using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MicroservicesENPS.CompanyServices.DTOs;
using MicroservicesENPS.CompanyServices.Entities;
using MongoDB.Driver;

namespace MicroservicesENPS.CompanyServices.Repositories.Interfaces
{
    public class CompanyReposiory : ICompanyReposiory
    {

        private const string collectionName = nameof(Company);
        private readonly IMongoCollection<Company> _iMongoCollection;
        public readonly FilterDefinitionBuilder<Company> filterDefinitionBuilder = Builders<Company>.Filter;

        public CompanyReposiory(IMongoDatabase iMongoDatabase)
        {
            _iMongoCollection = iMongoDatabase.GetCollection<Company>(collectionName);
        }

        public Task<List<Company>> GetAllAsync(CompanyFilterDTO companyFilter)
        {
            throw new NotImplementedException();
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