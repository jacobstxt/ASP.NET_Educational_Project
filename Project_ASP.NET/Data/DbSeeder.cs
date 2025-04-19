using System.Text.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project_ASP.NET.Data.Entities;
using Project_ASP.NET.DataBase.Entities;
using Project_ASP.NET.Models.Seeder;

namespace Project_ASP.NET.Data
{
    public static class DbSeeder
    {
        public static async Task SeedData(this WebApplication webApplication)
        {
            using var scope = webApplication.Services.CreateScope();
            //Цей об'єкт буде верта посилання на конткетс, який зараєстрвоано в Progran.cs
            var context = scope.ServiceProvider.GetRequiredService<ProjectDbContext>();
            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

            context.Database.Migrate();

            if (!context.Categories.Any())
            {
                var jsonFile = Path.Combine(Directory.GetCurrentDirectory(), "JsonData", "Categories.json");
                if (File.Exists(jsonFile))
                {
                    var jsonData = await File.ReadAllTextAsync(jsonFile);
                    try
                    {
                        var categories = JsonSerializer.Deserialize<List<SeederCategoryModal>>(jsonData);
                        var categoryEntities = mapper.Map<List<CategoryEntity>>(categories);
                        await context.AddRangeAsync(categoryEntities);
                        await context.SaveChangesAsync();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error Json Parse Data {0}", ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Not Found File Categories.json");
                }
            }
        }

    }
}
