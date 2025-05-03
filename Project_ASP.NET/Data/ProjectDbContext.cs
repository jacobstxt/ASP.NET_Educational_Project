using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_ASP.NET.Data.Entities;
using Project_ASP.NET.Data.Entities.Identity;

namespace Project_ASP.NET.Data
{
    public class ProjectDbContext : IdentityDbContext<UserEntity, RoleEntity, int>
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> opt) : base(opt) { }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ProductImageEntity> ProductImages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // identity 
            modelBuilder.Entity<UserRoleEntity>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRoleEntity>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

        }
    }
}
