using ElearningMVC.MiddleWare;
using ElearningMVC.Models;
using ElearningMVC.Services;
using EmailSend.MiddleWare;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddConnections(options=>
    builder.Configuration.GetConnectionString("dbconn")
    );
builder.Services.AddScoped<ElearningContext>();
builder.Services.AddTransient<EmailSending, Email>();
//builder.Services.AddTransient<CourseInterface, CourseImplement>();
builder.Services.AddScoped<CourseInterface,CourseImplement>();
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(5);
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        option.LoginPath = "/Auth/SignIn";
        option.AccessDeniedPath = "/Auth/SignIn";
    })
  //.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
  // {
	 //  options.ClientId = "806000584966-6tou4mcd0qh0r10ugkh8b3r7c8kmv0k8.apps.googleusercontent.com";
	 //  options.ClientSecret = "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";
  // });

builder.Services.AddHttpContextAccessor();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
