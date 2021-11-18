using System;

namespace MicroserviceENPS.Contracts
{
   public record CompanyCreated(Guid Id, string FantasyName, string Name);

   public record CompanyUpdated(Guid Id, string FatansyName, string Name);

   public record CompanyDeleted(Guid Id, string FatansyName, string Name);
}
