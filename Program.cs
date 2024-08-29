using ElearningMVC.MiddleWare;
using ElearningMVC.Models;
using ElearningMVC.Services;
using EmailSend.MiddleWare;

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();