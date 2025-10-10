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

builder.Services.AddDbContext<FirstExam.Data.AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

//scoped solo para db externo y singleton para db en memoria

builder.Services.AddScoped<FirstExam.Repositories.IPetRepository, FirstExam.Repositories.PetRepository>();
builder.Services.AddScoped<FirstExam.Services.IPetService, FirstExam.Services.PetService>();

builder.Services.AddScoped<FirstExam.Repositories.IOwnerRepository, FirstExam.Repositories.OwnerRepository>();
builder.Services.AddScoped<FirstExam.Services.IOwnerService, FirstExam.Services.OwnerService>();

builder.Services.AddScoped<FirstExam.Repositories.IAppointmentRepository, FirstExam.Repositories.AppointmentRepository>();
builder.Services.AddScoped<FirstExam.Services.IAppointmentService, FirstExam.Services.AppointmentService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("MiPoliticaCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
