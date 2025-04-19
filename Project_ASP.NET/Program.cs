using Microsoft.EntityFrameworkCore;
using Project_ASP.NET.Data;
using Project_ASP.NET.DataBase;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<ProjectDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("MyConnection")));


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


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
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Categories}/{action=Index}/{id?}")
    .WithStaticAssets();


await app.SeedData();

app.Run();//������
