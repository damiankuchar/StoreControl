using StoreControl.Domain.Common;

namespace StoreControl.Domain.Entities
{
    public class User : BaseAuditableEntity
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
