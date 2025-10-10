using FirstExam.Repositories;
using FirstExam.Services;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// OpenAPI estilo magíster (requiere paquete Microsoft.AspNetCore.OpenApi)

// DI: Appointments (repo en memoria = Singleton, service = Scoped)
builder.Services.AddSingleton<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
