using StoreControl.Domain.Common;

namespace StoreControl.Domain.Entities
{
    public class Permission : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
