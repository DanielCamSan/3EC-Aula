using FirstExam.Data;
using FirstExam.Repositories;
using FirstExam.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- Configuración de CORS ---
builder.Services.AddCors(
    options =>
    {
        options.AddPolicy("MiPoliticaCors", policy =>
        {
            policy.WithOrigins("https://localhost:7162", "http://127.0.0.1:5500").AllowAnyMethod().AllowAnyHeader();
        });
    });

// --- Añadir DbContext ---
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// --- Registrar Servicios y Repositorios ---
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IOwnerService, OwnerService>();

builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IPetService, PetService>();


builder.Services.AddControllers();

var app = builder.Build();

// --- Pipeline de Middlewares ---
app.UseHttpsRedirection();
app.UseCors("MiPoliticaCors");
app.UseAuthorization();
app.MapControllers();
app.Run();