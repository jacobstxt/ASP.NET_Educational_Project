using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_ASP.NET.Constants;
using Project_ASP.NET.Data.Entities;
using Project_ASP.NET.Data.Entities.Identity;
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

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();

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



            if (!context.Roles.Any())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();
                var admin = new RoleEntity { Name = Roles.Admin };
                var result = await roleManager.CreateAsync(admin);
                if (result.Succeeded)
                {
                    Console.WriteLine($"Роль {Roles.Admin} створено успішно");
                }
                else
                {
                    Console.WriteLine($"Помилка створення ролі:");
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"- {error.Code}: {error.Description}");
                    }
                }

                var user = new RoleEntity { Name = Roles.User };
                result = await roleManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    Console.WriteLine($"Роль {Roles.User} створено успішно");
                }
                else
                {
                    Console.WriteLine($"Помилка створення ролі:");
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"- {error.Code}: {error.Description}");
                    }
                }

            }

            if (!context.Users.Any())
            {
                string email = "admin@gmail.com";
                var user = new UserEntity
                {
                    UserName = email,
                    Email = email,
                    Name = "Марко",
                    Surname = "Онутрій"
                };

                var result = await userManager.CreateAsync(user, "123456");
                if (result.Succeeded)
                {
                    Console.WriteLine($"Користувача успішно створено {user.Surname} {user.Name}!");
                    await userManager.AddToRoleAsync(user, Roles.Admin);
                }
                else
                {
                    Console.WriteLine($"Помилка створення користувача:");
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"- {error.Code}: {error.Description}");
                    }
                }
            }







        }

    }
}
