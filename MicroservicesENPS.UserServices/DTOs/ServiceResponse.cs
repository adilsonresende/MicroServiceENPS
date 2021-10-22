using MicroserviceENPS.UserServices.Messages;

namespace MicroserviceENPS.UserServices.DTOs
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = PtBrMessages.OperationCompletedSuccessfully;
    }
}