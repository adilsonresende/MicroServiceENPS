using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroserviceENPS.UserServices.Entities;
using MongoDB.Driver;

namespace MicroserviceENPS.UserServices.Repositories.Interfaces
{
    public class UserRepository : IUserRepository
    {
        private const string collectionName = nameof(User);
        private readonly IMongoCollection<User> _iMongoCollection;
        public readonly FilterDefinitionBuilder<User> filterDefinitionBuilder = Builders<User>.Filter;

        public UserRepository(IMongoDatabase iMongoDatabase)
        {
            _iMongoCollection = iMongoDatabase.GetCollection<User>(collectionName);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            FilterDefinition<User> filterDefinition = filterDefinitionBuilder.Eq(x => x.Email, email);
            return await _iMongoCollection.Find(filterDefinition).FirstOrDefaultAsync();
        }

        public async Task<Guid> InsertAsync(User user)
        {
            await _iMongoCollection.InsertOneAsync(user);
            return user.Id;
        }

        public async Task LogicalDeleteAsync(Guid idUser)
        {
            FilterDefinition<User> filterDefinition = filterDefinitionBuilder.Eq(x => x.Id, idUser);
            User user = await _iMongoCollection.Find(filterDefinition).FirstOrDefaultAsync();
            user.SetActiveToFalse();
            await _iMongoCollection.ReplaceOneAsync(filterDefinition, user);
        }

        public async Task UpdateAsync(User user)
        {
            FilterDefinition<User> filterDefinition = filterDefinitionBuilder.Eq(x => x.Id, user.Id);
            await _iMongoCollection.ReplaceOneAsync(filterDefinition, user);
        }
    }
}