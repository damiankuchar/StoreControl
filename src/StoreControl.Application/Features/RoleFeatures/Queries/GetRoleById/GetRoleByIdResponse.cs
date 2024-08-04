namespace StoreControl.Application.Features.RoleFeatures.Queries.GetRoleById
{
    public class GetRoleByIdResponse
    {
        public Guid Id { get; set; }
        public string Name {  get; set; } = string.Empty;

        public IEnumerable<RoleResponsePermission> Permissions { get;set; } = new List<RoleResponsePermission>();
    }

    public class RoleResponsePermission
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
