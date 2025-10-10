using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

<<<<<<< HEAD
<<<<<<< HEAD
// Add services to the container.
=======
=======
>>>>>>> c634eb6a0b00698e41addd33859a9fd9d5b0dc9b
builder.Services.AddCors(
    options =>
    {
        options.AddPolicy("MiPoliticaCors", policy =>
        {
            policy.WithOrigins("https://localhost:7162", "http://127.0.0.1:5500").AllowAnyMethod().AllowAnyHeader();
        });
    });

<<<<<<< HEAD
>>>>>>> 2adf0a928d82def5e5aa21791a2224cd88eee797
=======
=======
// Add services to the container.
>>>>>>> 99e531e4d6daf8c647512293ca4090505b2f442b
>>>>>>> c634eb6a0b00698e41addd33859a9fd9d5b0dc9b
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddDbContext<apiwithdb.Data.AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Scoped solo para DB externo y singleton para DB en memoria

// Inyecciones para Owner
builder.Services.AddScoped<apiwithdb.Repositories.IOwnerRepository, apiwithdb.Repositories.OwnerRepository>();
builder.Services.AddScoped<apiwithdb.Services.IOwnerService, apiwithdb.Services.OwnerService>();

builder.Services.AddDbContext<FirstExam.Data.AppDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<FirstExam.Services.IAppointmentService, FirstExam.Services.AppointmentService>();
builder.Services.AddScoped<FirstExam.Repositories.IAppointmentRepository, FirstExam.Repositories.AppointmentRepository>();


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
