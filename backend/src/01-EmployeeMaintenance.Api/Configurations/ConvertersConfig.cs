using EmployeeMaintenance.Api.Converters;

namespace EmployeeMaintenance.Api.Configurations
{
    public static class ConvertersConfig
    {
        public static void AddConverters(this IServiceCollection services)
        {
            services.AddScoped<IActionResultConverter, ActionResultConverter>();
        }
    }
}