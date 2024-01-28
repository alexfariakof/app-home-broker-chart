using Business;
using Business.Interfaces;
using Repository;
using Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(c =>
{
    c.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();

    });
});

builder.Services.AddControllers();
builder.Services.AddScoped(typeof(IHomeBrokerBusiness), typeof(HomeBrokerBusiness));
builder.Services.AddScoped(typeof(IHomeBrokerRepository), typeof(HomeBrokerRepository));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
app.UseHsts();

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
app.UseAuthorization();
app.MapControllers();
app.UseDefaultFiles();
app.UseStaticFiles();
app.Run();