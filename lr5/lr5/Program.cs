
using lr5;
using lr5.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddSingleton<MyDataService>(new MyDataService());
builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("mysql")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "lab1",
        pattern: "{controller=Labs}/{action=RunLab1}");
    endpoints.MapControllerRoute(
        name: "lab2",
        pattern: "{controller=Labs}/{action=RunLab2}");
    endpoints.MapControllerRoute(
        name: "lab3",
        pattern: "{controller=Labs}/{action=RunLab3}");
    endpoints.MapControllerRoute(
        name: "Login",
        pattern: "{controller=Account}/{action=Login}");
    endpoints.MapControllerRoute(
        name: "Register",
        pattern: "{controller=Account}/{action=Register}");
});

app.Run();
