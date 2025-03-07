using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeMaintenance.Domain.Entities
{
    public class Employee : BaseEntity
    {
        [Required]
        [Column("hire_date")]
        public DateTime HireDate { get; private set; }

        [ForeignKey("Department")]
        [Column("department_id")]
        public Guid DepartmentId { get; private set; }

        public virtual Department Department { get; private set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public Guid UserId { get; private set; }

        public virtual User User { get; private set; }
    }
}