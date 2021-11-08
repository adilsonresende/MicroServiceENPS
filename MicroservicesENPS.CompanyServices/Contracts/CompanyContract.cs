using System;

namespace MicroservicesENPS.CompanyServices.Contracts
{
    public record CompanyCreated(Guid Id, string FantasyName, string Name);
    public record CompanyUpdated(Guid Id, string FantasyName, string Name);
    public record CompanyDeleted(Guid Id, string FantasyName, string Name);
}