namespace EmployeeMaintenance.Application.DTOs.Response
{
    public class UserResposeDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public AddressResposeDto Address { get; set; }
    }
}