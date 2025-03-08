using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeMaintenance.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; private set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; private set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; private set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; private set; }

        [Column("deleted_at")]
        public DateTime? DeletedAt { get; private set; }

        public void SoftDelete()
        {
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
        }

        public void SetCreatedAt(DateTime createdAt)
            => CreatedAt = createdAt;

        public void SetUpdatedAt(DateTime updatedAt)
            => UpdatedAt = updatedAt;
    }
}