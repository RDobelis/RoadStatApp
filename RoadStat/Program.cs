using Microsoft.EntityFrameworkCore;
using RoadStat.Core.Models;
using RoadStat.Core.Services;
using RoadStat.Data;
using RoadStat.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<RoadStatDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("road-stat"), 
        new MySqlServerVersion(new Version(8, 0, 32))));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", 
        builder =>
        { 
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddScoped<IRoadStatDbContext, RoadStatDbContext>();
builder.Services.AddScoped<IEntityService<CarSpeedEntry>, EntityService<CarSpeedEntry>>();
builder.Services.AddScoped<IFileParser<CarSpeedEntry>, CarSpeedEntryFileParser>();


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
