using FirstExam.Data;
using FirstExam.Repositories;
using FirstExam.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(
    options =>
    {
        options.AddPolicy("MiPoliticaCors", policy =>
        {
            policy.WithOrigins("https://localhost:7162", "http://127.0.0.1:5500").AllowAnyMethod().AllowAnyHeader();
        });
    });

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPetRepository, PetRepository>();

builder.Services.AddScoped<IPetService, PetService>();


builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("MiPoliticaCors");

app.UseAuthorization();

app.MapControllers();

app.Run();