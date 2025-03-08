namespace EmployeeMaintenance.Application.DTOs.Request
{
    public class UserRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public AddressRequestDto Address { get; set; }
    }
}