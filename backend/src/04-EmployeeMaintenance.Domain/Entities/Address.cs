using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeMaintenance.Domain.Entities
{
    public class Address : BaseEntity
    {
        public Address()
        {
        }

        public Address(string street, string city, string state, string zipCode)
        {
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        [Required]
        [MaxLength(255)]
        [Column("street")]
        public string Street { get; private set; }

        [Required]
        [MaxLength(100)]
        [Column("city")]
        public string City { get; private set; }

        [Required]
        [MaxLength(100)]
        [Column("state")]
        public string State { get; private set; }

        [Required]
        [MaxLength(20)]
        [Column("zip_code")]
        public string ZipCode { get; private set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public Guid UserId { get; private set; }
    }
}