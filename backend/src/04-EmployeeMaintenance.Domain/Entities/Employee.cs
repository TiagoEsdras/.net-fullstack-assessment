using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeMaintenance.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public Employee()
        {
        }

        public Employee(DateTime hireDate, Guid userId, Guid departmentId)
        {
            HireDate = hireDate;
            UserId = userId;
            DepartmentId = departmentId;
        }

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

        public void UpdateDepartment(Department department)
        {
            Department = department;
            DepartmentId = department.Id;
        }
    }
}