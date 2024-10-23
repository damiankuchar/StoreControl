namespace StoreControl.Application.Features.UsersFeatures
{
    public class UserDetailedDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
        public List<UserRoleDto> Roles { get; set; } = new List<UserRoleDto>();
    }

    public class UserRoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
