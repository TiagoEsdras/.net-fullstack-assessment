using EmployeeMaintenance.Api.Configurations;
using EmployeeMaintenance.Application.Mappings;
using EmployeeMaintenance.Application.Queries.Departments;
using EmployeeMaintenance.Infra.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddControllers(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
})
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EmployeeMaintenanceContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        it => it.MigrationsAssembly(typeof(EmployeeMaintenanceContext).Assembly.FullName)
    )
);
builder.Services.AddAutoMapper(typeof(MapperProfileConfig));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetDepartmentByNameQuery).Assembly));
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddConverters();
builder.Services.AddExceptions();
builder.Services.AddValidators();
builder.Services.AddProblemDetails();
builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 5242880;
});

var app = builder.Build();

app.UseStaticFiles();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(cors => cors.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin()
        .WithExposedHeaders("x-current-page", "x-page-size", "x-total-count", "x-total-pages"));

app.MapControllers();

app.UseExceptionHandler();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EmployeeMaintenanceContext>();
    dbContext.Database.Migrate();
}

app.Run();