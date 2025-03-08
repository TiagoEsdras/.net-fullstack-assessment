using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeMaintenance.Domain.Entities
{
    public class Department : BaseEntity
    {
        public Department()
        {
        }

        public Department(string name)
        {
            Name = name;
        }

        [Required]
        [MaxLength(100)]
        [Column("name")]
        public string Name { get; private set; }
    }
}