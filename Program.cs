using FirstExam.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<FirstExam.Repositories.IPetRepository, FirstExam.Repositories.PetRepository>();
builder.Services.AddScoped<FirstExam.Services.IPetService, FirstExam.Services.PetService>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConection"));
});
builder.Services.AddCors(
    options =>
    {
        options.AddPolicy("MiPoliticaCors", policy =>
        {
            policy.WithOrigins("https://localhost:7162", "http://127.0.0.1:5500").AllowAnyMethod().AllowAnyHeader();
        });
    });


builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("MiPoliticaCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
