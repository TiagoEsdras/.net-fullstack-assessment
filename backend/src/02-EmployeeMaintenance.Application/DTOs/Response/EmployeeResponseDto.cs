namespace EmployeeMaintenance.Application.DTOs.Response
{
    public class EmployeeResponseDto
    {
        public Guid Id { get; set; }
        public DateTime HireDate { get; set; }
        public DepartmentResponseDto Department { get; set; }
        public Guid DepartmentId { get; set; }
        public UserResponseDto User { get; set; }
        public Guid UserId { get; set; }
    }
}