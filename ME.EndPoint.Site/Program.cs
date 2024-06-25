using ME.Application.Interfaces;
using ME.Application.Resources;
using ME.DataSource.Contexts;
using ME.EndPoint.Site;
using ME.EndPoint.Site.Resources;
using ME.Entities.Interfaces.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Models.UserControls.Enums;
using System.Collections.Generic;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddControllersWithViews()
#region Multi-Culture
     .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix) // Multi-Culture
        .AddDataAnnotationsLocalization(); //Multi-Culture
#endregion

#region Multi-Culture
//https://learn.microsoft.com/en-us/aspnet/core/fundamentals/localization/select-language-culture?view=aspnetcore-8.0
//https://muratsuzen.medium.com/adding-multiple-languages-with-asp-net-core-mvc-c1cb85929bed
//https://stackoverflow.com/questions/77615351/shared-localization-resource-file-in-razor
builder.Services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });


var supportedCultures = new[] { "fa-IR", "en-US"}
    .Select(culture => new CultureInfo(culture))
    .ToList();


builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(supportedCultures.FirstOrDefault());
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

#endregion

#region Database setup
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

if (!string.IsNullOrEmpty(sqlProviderTypeString))
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

#endregion

builder.Services.AddSingleton<IStringLocalizerResource, SharedResources>();

var app = builder.Build();

#region Multi-Culture
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
#endregion

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
