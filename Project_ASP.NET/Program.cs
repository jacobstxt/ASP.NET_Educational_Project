using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Project_ASP.NET.Data;
using Project_ASP.NET.Data.Entities;
using Project_ASP.NET.Data.Entities.Identity;
using Project_ASP.NET.Interfaces;
using Project_ASP.NET.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<ProjectDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("MyConnection")));



builder.Services.AddIdentity<UserEntity, RoleEntity>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
})
    .AddEntityFrameworkStores<ProjectDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();

// Add services to the container.
//У нас будуть View - це такі сторінки, де можна писати на C# Index.cshtml
//Велика перевага цих сторінок у тому, що вони перевіряються на c# і компілюються у збірку
//Project_ASP.NET.dll - вихідний файл проекту.
//контролер - це клас на C# , який приймає запити від клієнта і виконує усю логіку роботи.
//Результат роботи (Model) контролера передають на View для відображення
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();//Підтримка маршрутизації - це коли ми можемо писати в url, наприклад: localhost:2345/login

app.UseAuthorization();//Підтримка авторизації - це будемо вивчати коли перейдемо на до Identity


app.MapStaticAssets();//Використання статичних файлів , тобто у нас буде працювати папка wwwroot

//Налаштування для маршрутів. У нас є контролери - вони мають називатися HomeController
//При цьому враховується лише Home. Методи цього класу називаються Action - тобто обробники
//Для того щоб при запуску сайту ми бачили , щось визивається згідно налаштувань HomeController
//і його метод Index при цьому може бути параметр у маршруті id - але там є знак питання, тобто
//може бути null


app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
      name: "admin_area",
      areaName: "Admin",
      pattern: "admin/{controller=Users}/{action=Index}/{id?}"
    );
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Categories}/{action=Index}/{id?}"
    );
});





var dir = builder.Configuration["ImagesDir"];
string path = Path.Combine(Directory.GetCurrentDirectory(), dir);
Directory.CreateDirectory(path);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(path),
    RequestPath = $"/{dir}"
});

await app.SeedData();

app.Run();//Запуск
