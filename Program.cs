using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//
builder.Services.AddDbContext<FirstExam.Data.AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Default")));


//scoped solo para db externo y singleton para db en memoria

builder.Services.AddScoped<FirstExam.Repositories.IOwnerRepository, FirstExam.Repositories.OwnerRepository>();
builder.Services.AddScoped<FirstExam.Services.IOwnerService, FirstExam.Services.OwnerService>();

//

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
