using FirstExam.Controllers;
using FirstExam.Repositories;
using FirstExam.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

//builder.Services.AddOpenApi();

builder.Services.AddDbContext<FirstExam.Data.AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Scoped solo para DB externo y singleton para DB en memoria
builder.Services.AddDbContext<FirstExam.Data.AppDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Inyecciones para Owner
builder.Services.AddScoped<FirstExam.Repositories.IOwnerRepository, FirstExam.Repositories.OwnerRepository>();
builder.Services.AddScoped<FirstExam.Services.IOwnerService, FirstExam.Services.OwnerService>();

builder.Services.AddScoped<FirstExam.Services.IAppointmentService, FirstExam.Services.AppointmentService>();
builder.Services.AddScoped<FirstExam.Repositories.IAppointmentRepository, FirstExam.Repositories.AppointmentRepository>();

builder.Services.AddScoped<FirstExam.Services.IPetService, FirstExam.Services.PetService>();
builder.Services.AddScoped<FirstExam.Repositories.IPetRepository, FirstExam.Repositories.PetRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
*/
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
