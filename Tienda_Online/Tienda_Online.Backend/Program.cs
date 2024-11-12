using Microsoft.EntityFrameworkCore;
using Tienda_Online.Backend.Clases;
using Tienda_Online.Backend.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Conexión a la base de datos
builder.Services.AddDbContext<DataContext>(x=>x.UseSqlServer("name=DockerConnection"));

//Inyectamos las clases de los servicios
builder.Services.AddScoped<clsProducto>();
builder.Services.AddScoped<clsPromocionProducto>();
builder.Services.AddScoped<clsCarritoCompra>();
builder.Services.AddScoped<clsFactura>();
builder.Services.AddScoped<clsInforme>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
   .AllowAnyMethod()
   .AllowAnyHeader()
   .SetIsOriginAllowed(origin => true)
   .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
