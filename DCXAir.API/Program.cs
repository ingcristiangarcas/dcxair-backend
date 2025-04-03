using DCXAir.Domain.Interfaces;
using DCXAir.Infrastructure.Repositories;
using DCXAir.Infrastructure.Services;
using DCXAir.Application.Services;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Asegurarse de que el directorio Data existe
var dataDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Data");
if (!Directory.Exists(dataDirectory))
{
    Directory.CreateDirectory(dataDirectory);
}

// Asegurarse de que el archivo markets.json existe
var jsonFilePath = Path.Combine(dataDirectory, "markets.json");
if (!File.Exists(jsonFilePath))
{
    // Crear un archivo JSON vacío o con datos de ejemplo
    File.WriteAllText(jsonFilePath, "[]");
    // Nota: En un caso real, deberías proporcionar datos reales o mostrar un error
}

// Register services
builder.Services.AddSingleton<IFlightRepository>(provider => 
    new FlightRepository(jsonFilePath));
builder.Services.AddScoped<ICurrencyConverter, CurrencyConverter>();
builder.Services.AddScoped<IRouteService, RouteService>();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", builder =>
    {
        builder.WithOrigins("http://localhost:4200") // Asegúrate de que este es el origen correcto
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials(); // Añade esta línea
    });
});


var app = builder.Build();

// Configurar el middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Añade esta línea
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilitar CORS antes de usar routing
app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.UseAuthorization();
app.MapControllers();;

app.Run();

