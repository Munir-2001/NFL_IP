using DataModels;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Implementations;
using Repositories.Interfaces;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container or dependency injections

builder.Services.AddDbContext<myappcontext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("MVCconnstring")));

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
builder.Services.AddScoped<Iipobject, IPObject>();  
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}


app.UseCors("AllowAll");

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
