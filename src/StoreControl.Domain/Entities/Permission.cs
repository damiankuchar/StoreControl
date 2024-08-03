using StoreControl.Domain.Common;

namespace StoreControl.Domain.Entities
{
    public class Permission : IEntity, IAuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
