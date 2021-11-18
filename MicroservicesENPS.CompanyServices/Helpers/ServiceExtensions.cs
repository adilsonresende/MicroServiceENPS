using System;
using System.Reflection;
using GreenPipes;
using MassTransit;
using MassTransit.Definition;
using MicroservicesENPS.CompanyServices.Repositories.Interfaces;
using MicroservicesENPS.CompanyServices.Services;
using MicroservicesENPS.CompanyServices.Services.Interfaces;
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
                MongoDbSettings mongoDbSettings = iConfiguration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
                MongoClient mongoClient = new MongoClient(mongoDbSettings.ConnectionString);
                return mongoClient.GetDatabase(serviceSettings.ServiceName);
            });

            return services;
        }

        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddSingleton<ICompanyRepository, CompanyRepository>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<ICompanyService, CompanyService>();
            return services;
        }
    }
}