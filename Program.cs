using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using prueba_brayan_caviedes.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PruebaBrayanCaviedesContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
});


//   CONFIGURACION DE CORS (API REST) - HABILITAR CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PoliticaHabilitarCors", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();

    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();


app.UseCors("PoliticaHabilitarCors");

app.Run();
