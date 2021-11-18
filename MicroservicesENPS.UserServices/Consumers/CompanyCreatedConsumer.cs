using System;
using System.IO;
using System.Threading.Tasks;
using MassTransit;
using MicroserviceENPS.Contracts;

namespace MicroservicesENPS.UserServices.Consumers
{
    public class CompanyCreatedConsumer : IConsumer<CompanyCreated>
    {
        public async Task Consume(ConsumeContext<CompanyCreated> context)
        {
            var message = context.Message;
            await Task.Delay(1);

            Guid guid = message.Id;
            string FantasyName = message.FantasyName;
            string Name = message.Name;

            string path = @"D:\Temp\RabbitMQTest.txt";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine($"Id: {guid} FantasyName: {FantasyName} Name: {Name}");
                }
            }
        }
    }
}