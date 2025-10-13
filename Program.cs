using FirstExam.Data;
using FirstExam.Repositories;
using FirstExam.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IPetService, PetService>();

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("MiPoliticaCors", policy =>
    {
        policy.WithOrigins("https://localhost:7162", "http://127.0.0.1:5500")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
// Add services to the container.
builder.Services.AddControllers();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MiPoliticaCors", policy =>
    {
        policy.WithOrigins("https://localhost:7162", "http://127.0.0.1:5500")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configuración de Entity Framework para PostgreSQL (igual que tu docente)
builder.Services.AddDbContext<FirstExam.Data.AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Dependency Injection (exactamente el mismo patrón)
builder.Services.AddScoped<FirstExam.Repositories.IAppointmentRepository, FirstExam.Repositories.AppointmentRepository>();
builder.Services.AddScoped<FirstExam.Services.IAppointmentService, FirstExam.Services.AppointmentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseCors("MiPoliticaCors");
app.UseAuthorization();
app.MapControllers();

app.Run();