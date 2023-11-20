using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//modification
builder.Services.AddDbContext<BaseDbContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")
    ));
builder.Services.AddScoped<IUserRepository, UserRepository>(); //ioc records
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<IExcursionRepository, ExcursionRepository>();

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
