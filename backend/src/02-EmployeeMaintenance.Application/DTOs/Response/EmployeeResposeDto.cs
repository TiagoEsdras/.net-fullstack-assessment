namespace EmployeeMaintenance.Application.DTOs.Response
{
    public class EmployeeResposeDto
    {
        public Guid Id { get; set; }
        public DateTime HireDate { get; set; }
        public DepartmentResponseDto Department { get; set; }
        public UserResposeDto User { get; set; }
    }
}