using ME.DataSource.Contexts;
using ME.EndPoint.Site;
using ME.Entities.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<IDataBaseContext, MySelfStudyDictionary2Db>(options =>
{
    ConfigurationManager configuration = new ConfigurationManager();
#if DEBUG
    configuration.AddJsonFile("appsettings.Development.json");
#else
    configuration.AddJsonFile("appsettings.json");
#endif

    SQLProviderType providerType = SQLProviderType.MySql;
    string? sqlProviderTypeString = configuration["Database:ProviderType"]?.ToString();

    if (string.IsNullOrEmpty(sqlProviderTypeString))
    {
        providerType = (SQLProviderType)Enum.Parse(typeof(SQLProviderType), sqlProviderTypeString, true);
    }


    string? connectionString = string.Empty;
    if (providerType == SQLProviderType.MySql)
    {
        connectionString = configuration["Database:ConnectionString_" + providerType]?.ToString();
        options.UseMySQL(connectionString);

    }
    else if (providerType == SQLProviderType.SQLServer)
    {
        connectionString = configuration["Database:ConnectionString_" + providerType]?.ToString();
        options.UseSqlServer(connectionString);
        
    }
    else
        throw new NotImplementedException();
    

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
