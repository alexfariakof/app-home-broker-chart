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
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseDefaultFiles();
app.UseStaticFiles();
app.Run();