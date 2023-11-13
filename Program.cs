using AlturaTerminales.Code;
using AlturaTerminales.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
 
builder.Services.AddDbContext<QainspeccionContext>(options => 
                 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConection")));
  
var app = builder.Build();
 
app.UseHttpsRedirection();
 
app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Image")),
    RequestPath = new PathString("/Terminales")
});


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
