using Microsoft.AspNetCore.Identity;

namespace Project_ASP.NET.Data.Entities.Identity
{
    public class UserRoleEntity:IdentityUserRole<int>
    {
        public UserEntity User { get; set; } = new();
        public RoleEntity Role { get; set; } = new();
    }

}
