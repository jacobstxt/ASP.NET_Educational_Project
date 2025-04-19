using Microsoft.EntityFrameworkCore;
using Project_ASP.NET.Data.Entities;
using Project_ASP.NET.DataBase.Entities;

namespace Project_ASP.NET.DataBase;

public class ASP_ProjectDbContext: DbContext
{
    public ASP_ProjectDbContext(DbContextOptions<ASP_ProjectDbContext> opt) :  base(opt) {}
    public DbSet<CategoryEntity> Categories { get; set; }
}
