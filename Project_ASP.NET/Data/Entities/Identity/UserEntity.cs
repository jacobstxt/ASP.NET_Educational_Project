using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Project_ASP.NET.Data.Entities.Identity
{
    public class UserEntity: IdentityUser<int>
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } 
        [Required]
        [MaxLength(150)]
        public string Surname { get; set; } 
        [Required, StringLength(255)]
        public string AvatarUrl { get; set; }

        public ICollection<UserRoleEntity>? UserRoles { get; set; }
    }
}
