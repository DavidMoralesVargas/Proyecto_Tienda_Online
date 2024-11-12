using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tienda_Online.Backend.Clases;
using Tienda_Online.Backend.Data;
using Tienda_Online.Shared.Entidades;

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
builder.Services.AddScoped<clsUsuario>();


builder.Services.AddIdentity<Usuario, IdentityRole>(x =>
{
    x.User.RequireUniqueEmail = false;
    x.Password.RequireDigit = false;
    x.Password.RequiredUniqueChars = 0;
    x.Password.RequireLowercase = false;
    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequireUppercase = false;
    x.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<DataContext>()
.AddDefaultTokenProviders();

builder.Services.AddTransient<SeedDb>();

var app = builder.Build();



SeedData(app);

static void SeedData(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory!.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<SeedDb>();
        service!.SeedAsync().Wait();
    }
}

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

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();
