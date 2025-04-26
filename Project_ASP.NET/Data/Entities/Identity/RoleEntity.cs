using Microsoft.AspNetCore.Identity;

namespace Project_ASP.NET.Data.Entities.Identity
{
    public class RoleEntity:IdentityRole<int>
    {
        public ICollection<UserRoleEntity>? UserRoles { get; set; }
    }
}
