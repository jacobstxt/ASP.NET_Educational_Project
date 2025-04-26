using Microsoft.EntityFrameworkCore;
using Project_ASP.NET.Data.Entities;

namespace Project_ASP.NET.Data
{
    public class ProjectDbContext:DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> opt) : base(opt) { }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<UserEntity> User { get; set; }
    }
}
