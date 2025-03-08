using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeMaintenance.Domain.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
        }

        public User(string firstName, string lastName, string phone, Address address)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Address = address;
        }

        [Required]
        [MaxLength(100)]
        [Column("first_name")]
        public string FirstName { get; private set; }

        [Required]
        [MaxLength(100)]
        [Column("last_name")]
        public string LastName { get; private set; }

        [Required]
        [MaxLength(20)]
        [Column("phone")]
        public string Phone { get; private set; }

        public virtual Address Address { get; private set; }
    }
}