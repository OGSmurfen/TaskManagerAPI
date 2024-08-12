using Microsoft.EntityFrameworkCore;
using TaskManagerAPI_2.Repository.RepositoryImpl;
using TaskManagerAPI_2.Data;
using TaskManagerAPI_2.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Db Context 
builder.Services.AddScoped<TaskDbContext>();
builder.Services.AddDbContext<TaskDbContext>(opt => opt.UseNpgsql("Name=ConnectionStrings:db"));

builder.Services.AddScoped(typeof(IRepository<>), typeof(RepositoryImpl<>));

builder.Services.AddScoped(typeof(ITasksRepository), typeof(TasksRepository));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
