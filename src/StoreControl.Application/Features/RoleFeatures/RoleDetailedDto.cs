﻿namespace StoreControl.Application.Features.RoleFeatures
{
    public class RoleDetailedDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<RolePermissionDto> Permissions { get; set; } = new List<RolePermissionDto>();
    }

    public class RolePermissionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
