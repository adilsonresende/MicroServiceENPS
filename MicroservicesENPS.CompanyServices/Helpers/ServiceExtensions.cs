using MicroservicesENPS.CompanyServices.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace MicroservicesENPS.CompanyServices.Helpers
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new ByteArraySerializer(BsonType.Binary));

            services.AddSingleton(serviceProvider =>
            {
                IConfiguration iConfiguration = serviceProvider.GetService<IConfiguration>();
                ServiceSettings serviceSettings = iConfiguration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                var mongoDbSettings = iConfiguration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
                var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);
                return mongoClient.GetDatabase(serviceSettings.ServiceName);
            });
            
            return services;
        }
    }
}