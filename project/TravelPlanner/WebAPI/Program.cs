using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Service.Abstract;
using Service.Concrete;
using Service.Rules.Abstract;
using Service.Rules.Concrete;
using System.ComponentModel;


var builder = WebApplication.CreateBuilder(args);

//json datetime conversion for display purposes only
/*builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new WebAPI.Converters.DateTimeConverter("dd/MM/yyyy HH:mm"));
    });*/

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/*builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
    c.MapType<DateTime>(() => new OpenApiSchema { Type = "string", Format = "date-time", Example = new OpenApiString(DateTime.Now.ToString("dd/MM/yyyy HH:mm")) });
});*/

//modification
builder.Services.AddDbContext<BaseDbContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")
    ));

//ioc records
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<IExcursionRepository, ExcursionRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddScoped<IExcursionService, ExcursionService>();
builder.Services.AddScoped<IUserRules, UserRules>();  
builder.Services.AddScoped<ITripRules, TripRules>(); 
builder.Services.AddScoped<IExcursionRules, ExcursionRules>();

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
