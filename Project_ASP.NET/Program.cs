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
//� ��� ������ View - �� ��� �������, �� ����� ������ �� C# Index.cshtml
//������ �������� ��� ������� � ����, �� ���� ������������ �� c# � ����������� � �����
//Project_ASP.NET.dll - �������� ���� �������.
//��������� - �� ���� �� C# , ���� ������ ������ �� �볺��� � ������ ��� ����� ������.
//��������� ������ (Model) ���������� ��������� �� View ��� �����������
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();//ϳ������� ������������� - �� ���� �� ������ ������ � url, ���������: localhost:2345/login

app.UseAuthorization();//ϳ������� ����������� - �� ������ ������� ���� ��������� �� �� Identity


app.MapStaticAssets();//������������ ��������� ����� , ����� � ��� ���� ��������� ����� wwwroot

//������������ ��� ��������. � ��� � ���������� - ���� ����� ���������� HomeController
//��� ����� ����������� ���� Home. ������ ����� ����� ����������� Action - ����� ���������
//��� ���� ��� ��� ������� ����� �� ������ , ���� ���������� ����� ����������� HomeController
//� ���� ����� Index ��� ����� ���� ���� �������� � ������� id - ��� ��� � ���� �������, �����
//���� ���� null


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

app.Run();//������
