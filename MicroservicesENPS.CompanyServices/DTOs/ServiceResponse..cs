using MicroservicesENPS.CompanyServices.Messages;

namespace MicroservicesENPS.CompanyServices.DTOs
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = PtBrMessages.OperationCompletedSuccessfully;
    }
}