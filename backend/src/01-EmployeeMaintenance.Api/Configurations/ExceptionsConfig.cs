using EmployeeMaintenance.Api.ExceptionsHandler;

namespace EmployeeMaintenance.Api.Configurations
{
    public static class ExceptionsConfig
    {
        public static void AddExceptions(this IServiceCollection services)
        {
            services.AddExceptionHandler<ValidationExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
        }
    }
}