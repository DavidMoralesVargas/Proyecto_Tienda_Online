using Microsoft.EntityFrameworkCore;
using Tienda_Online.Backend.Clases;
using Tienda_Online.Backend.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Conexi�n a la base de datos
builder.Services.AddDbContext<DataContext>(x=>x.UseSqlServer("name=DockerConnection"));

//Inyectamos las clases �de los servicios
builder.Services.AddScoped<clsProducto>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
