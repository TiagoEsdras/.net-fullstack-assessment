namespace EmployeeMaintenance.Application.DTOs.Request
{
    public class EmployeeRequestDto
    {
        public DateTime HireDate { get; set; }
        public DepartmentRequestDto Department { get; set; }
        public UserRequestDto User { get; set; }
    }
}