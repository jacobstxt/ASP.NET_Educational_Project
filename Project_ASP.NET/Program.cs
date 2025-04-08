var builder = WebApplication.CreateBuilder(args);

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


app.Run();//������
