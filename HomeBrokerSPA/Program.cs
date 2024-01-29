using Business;
using Business.Interfaces;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.OpenApi.Models;
using Repository;
using Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var appName = "Home Broker Chart API";
var appVersion = "v1";
var appDescription = $"API para gerar dados históricos da variação de preços da Magazine Luiza S.A.em um determinado período.";

builder.Services.AddCors(c =>
{
    c.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();

    });
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddScoped(typeof(IHomeBrokerBusiness), typeof(HomeBrokerBusiness));
builder.Services.AddScoped(typeof(IHomeBrokerRepository), typeof(HomeBrokerRepository));
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

if (app.Environment.IsStaging()) {
    app.Urls.Add("http://0.0.0.0:3002");
    app.Urls.Add("https://0.0.0.0:3003");
}
else if (app.Environment.IsEnvironment("WatchMode"))
{
    app.Urls.Add("http://127.0.0.1:5000");
    app.Urls.Add("https://127.0.0.1:5001");
}
else
    app.UseHttpsRedirection();

app.UseCors();
app.UseHsts();

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json",  $"{appName} {appVersion}"); });
var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);

app.UseAuthorization();
app.MapControllers();
app.UseDefaultFiles();
app.UseStaticFiles();
app.Run();