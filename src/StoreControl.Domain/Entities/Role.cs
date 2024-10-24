using StoreControl.Domain.Common;

namespace StoreControl.Domain.Entities
{
    public class Role : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
