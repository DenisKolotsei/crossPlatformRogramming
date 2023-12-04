
using lr11;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<MyDataService>(new MyDataService());
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "MyCoocki";
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust as needed
    options.Cookie.HttpOnly = false;
    options.Cookie.IsEssential = false;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});
/*builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000") // Укажите свой фактический адрес клиента React
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});*/
/*builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
    options.HttpOnly = HttpOnlyPolicy.None;
    options.Secure = CookieSecurePolicy.Always; // Требуется использование HTTPS
});*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:3000")
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials();
});
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "profile",
        pattern: "{controller=Home}/{action=Profile}");
    endpoints.MapControllerRoute(
        name: "lab1",
        pattern: "{controller=Labs}/{action=Lab1}");
    endpoints.MapControllerRoute(
        name: "lab2",
        pattern: "{controller=Labs}/{action=Lab2}");
    endpoints.MapControllerRoute(
        name: "lab3",
        pattern: "{controller=Labs}/{action=Lab3}");
    endpoints.MapControllerRoute(
        name: "Login",
        pattern: "{controller=OAuth}/{action=GetUrl}");
    endpoints.MapControllerRoute(
        name: "Register",
        pattern: "{controller=Account}/{action=Code}");
});

app.Run();
