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

builder.Services.AddControllers();

builder.Services.AddDbContext<FirstExam.Data.AppDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<FirstExam.Services.IAppointmentService, FirstExam.Services.AppointmentService>();
builder.Services.AddScoped<FirstExam.Repositories.IAppointmentRepository, FirstExam.Repositories.AppointmentRepository>();


var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("MiPoliticaCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
