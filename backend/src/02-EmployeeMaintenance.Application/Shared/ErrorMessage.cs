namespace EmployeeMaintenance.Application.Shared
{
    public class ErrorMessage
    {
        public ErrorMessage(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; private set; }
        public string Message { get; private set; }
    }
}