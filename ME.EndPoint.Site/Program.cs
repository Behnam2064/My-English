using ME.Application.Services.AdoDotNets;
using ME.DataSource.Contexts;
using ME.Entities.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Add(new ServiceDescriptor(typeof(IFileStreamGenerator), typeof(FileStreamGenerator), ServiceLifetime.Transient));

builder.Services.AddDbContext<IDataBaseContext, MySelfStudyDictionary2Db>(options =>
{
    ConfigurationManager configuration = new ConfigurationManager();
#if DEBUG
    configuration.AddJsonFile("appsettings.Development.json");
#else
    configuration.AddJsonFile("appsettings.json");
#endif
    string? connectionString = configuration["Database:ConnectionString"]?.ToString();
    options.UseSqlServer(connectionString);
});

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
