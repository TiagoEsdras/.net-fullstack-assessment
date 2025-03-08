using EmployeeMaintenance.Application.Interfaces.Services;
using EmployeeMaintenance.Application.Services;

namespace EmployeeMaintenance.Api.Configurations
{
    public static class ServicesConfig
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
        }
    }
}