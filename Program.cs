
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddDbContext<FirstExam.Data.AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

//scoped solo para db externo y singleton para db en memoria

builder.Services.AddScoped<FirstExam.Repositories.IOwnerRepository, FirstExam.Repositories.OwnerRepository>();
builder.Services.AddScoped<FirstExam.Services.IOwnerService, FirstExam.Services.OwnerService>();
builder.Services.AddScoped<FirstExam.Services.IAppointmentServices, FirstExam.Services.AppointmentServices>();
builder.Services.AddScoped<FirstExam.Repositories.IAppointmentRepository, FirstExam.Repositories.AppointmentRepository>();
var app = builder.Build();
// Dependency Injection

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
