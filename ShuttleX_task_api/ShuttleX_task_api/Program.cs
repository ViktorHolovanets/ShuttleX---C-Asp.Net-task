using ShuttleX_task_api.Models;
using ShuttleX_task_api.Repositories.Interfaces;
using ShuttleX_task_api.Repositories;
using ShuttleX_task_api.Services.Interfaces.DB;
using ShuttleX_task_api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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
