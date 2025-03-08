using EmployeeMaintenance.Application.Interfaces.Repositories;
using EmployeeMaintenance.Infra.Repositories;

namespace EmployeeMaintenance.Api.Configurations
{
    public static class RepositoriesConfig
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}