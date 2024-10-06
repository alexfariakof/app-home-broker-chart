using Business;
using Business.Interfaces;
using Domain.Charts.Agreggates.Factory;
using HomeBroker.Domain.Charts.Agreggates.Factory;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.OpenApi.Models;
using Repository;
using Repository.Interfaces;
using OfficeOpenXml;

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

var builder = WebApplication.CreateBuilder(args);
var appName = "Home Broker Chart API";
var appVersion = "v1";
var appDescription = $"API para gerar dados históricos da variação de preços da Magazine Luiza S.A.em um determinado período.";

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddSingleton<IMagazineLuizaHistoryPriceFactory>(new MagazineLuizaHistoryPriceFactory());
builder.Services.AddScoped(typeof(IHomeBrokerRepository), typeof(HomeBrokerRepository));

builder.Services.AddSingleton<IHomeBrokerBusiness>(new HomeBrokerBusiness(
    builder?.Services?.BuildServiceProvider().GetService<IMagazineLuizaHistoryPriceFactory>() ?? throw new(),
    builder?.Services?.BuildServiceProvider().GetService<IHomeBrokerRepository>() ?? throw new()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc(appVersion,
    new OpenApiInfo
    {
        Title = appName,
        Version = appVersion,
        Description = appDescription,
        Contact = new OpenApiContact
        {
            Name = "Alex Ribeiro de Faria",
            Url = new Uri("https://github.com/alexfariakof/Home_Broker_Chart")
        }
    });
});

var app = builder.Build();

if (app.Environment.IsEnvironment("Swagger"))
{
    app.Urls.Add("http://127.0.0.1:5000");
    app.Urls.Add("https://127.0.0.1:5001");
}

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json",  $"{appName} {appVersion}"); });

if (app.Environment.IsEnvironment("Swagger"))
{
    var option = new RewriteOptions();
    option.AddRedirect("^$", "swagger");
    app.UseRewriter(option);
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();

//app.UseHttpsRedirection();
app.UseHsts();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints?.MapControllers();
    endpoints?.MapFallbackToFile("index.html");
});

app.Run();