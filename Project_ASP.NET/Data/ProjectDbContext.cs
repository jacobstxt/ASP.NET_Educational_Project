using Microsoft.EntityFrameworkCore;
using Project_ASP.NET.DataBase.Entities;

namespace Project_ASP.NET.Data
{
    public class ProjectDbContext:DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> opt) : base(opt) { }
        public DbSet<CategoryEntity> Categories { get; set; }
    }
}
