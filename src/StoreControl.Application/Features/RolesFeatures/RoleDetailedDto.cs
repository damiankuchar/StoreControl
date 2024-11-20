namespace StoreControl.Application.Features.RolesFeatures
{
    public class RoleDetailedDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<RolePermissionDto> Permissions { get; set; } = new List<RolePermissionDto>();
    }

    public class RolePermissionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
