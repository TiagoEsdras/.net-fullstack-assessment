namespace EmployeeMaintenance.Application.DTOs.Response
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string PhotoUrl { get; set; }
        public AddressResponseDto Address { get; set; }
        public Guid AddressId { get; set; }
    }
}