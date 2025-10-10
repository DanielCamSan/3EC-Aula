using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddDbContext<apiwithdb.Data.AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Scoped solo para DB externo y singleton para DB en memoria

// Inyecciones para Owner
builder.Services.AddScoped<apiwithdb.Repositories.IOwnerRepository, apiwithdb.Repositories.OwnerRepository>();
builder.Services.AddScoped<apiwithdb.Services.IOwnerService, apiwithdb.Services.OwnerService>();

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
