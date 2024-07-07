using ShuttleX_task_api.Models;
using ShuttleX_task_api.Repositories.Interfaces;
using ShuttleX_task_api.Repositories;
using ShuttleX_task_api.Services.Interfaces.DB;
using ShuttleX_task_api.Services;
using Microsoft.EntityFrameworkCore;
using ShuttleX_task_api.Hubs;
using ShuttleX_task_api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDb>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Repositories
builder.Services.AddScoped<IBaseRepository<User>, UserRepository>();
builder.Services.AddScoped<IBaseRepository<Message>, MessageRepository>();
builder.Services.AddScoped<IBaseRepository<Chat>, ChatRepository>();

// Services
builder.Services.AddScoped<IBaseService<Message>, BaseService<Message>>();
builder.Services.AddScoped<IBaseService<User>, BaseService<User>>();
builder.Services.AddScoped<IBaseService<Chat>, BaseService<Chat>>();

builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials()
           .SetIsOriginAllowed((host) => true);
}));

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseCors();
app.MapHub<ChatHub>("/api/chatHub");
app.UseAuthorization();

app.MapControllers();

app.Run();
