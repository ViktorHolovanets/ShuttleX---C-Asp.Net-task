using Microsoft.EntityFrameworkCore;
using My_one_day_life_api.Models;
using My_one_day_life_api.Repositories;
using My_one_day_life_api.Repositories.Interfaces;
using My_one_day_life_api.Services;
using My_one_day_life_api.Services.Interfaces.DB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDb>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Repositories
builder.Services.AddScoped<IEntityRepository<User>, UserRepository>();
builder.Services.AddScoped<IEntityRepository<Message>, MessageRepository>();
builder.Services.AddScoped<IEntityRepository<Chat>, ChatRepository>();

// Services
builder.Services.AddScoped<IBaseService<Message>, BaseService<Message>>();
builder.Services.AddScoped<IBaseService<User>, BaseService<User>>();
builder.Services.AddScoped<IBaseService<Chat>, BaseService<Chat>>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
