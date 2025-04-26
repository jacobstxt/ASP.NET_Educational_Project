using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ASP.NET.Data.Entities
{

    [Table("tbl_users")]
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(150)]
        public string Surname { get; set; } = string.Empty;
        [Required]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        [Required, StringLength(255)]
        public string AvatarUrl { get; set; } = string.Empty;
    }
}
