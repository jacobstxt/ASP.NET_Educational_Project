using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Project_ASP.NET.Data.Entities.Identity
{
    public class UserEntity: IdentityUser<int>
    {
        [MaxLength(150)]
        public string Name { get; set; } 
        [MaxLength(150)]
        public string Surname { get; set; } 
        [StringLength(255)]
        public string? AvatarUrl { get; set; }

        public ICollection<UserRoleEntity>? UserRoles { get; set; }
    }
}
