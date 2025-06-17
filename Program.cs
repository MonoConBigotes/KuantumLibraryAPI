using FluentValidation;
using FluentValidation.AspNetCore;
using KuantumLibraryApi.Data;
using KuantumLibraryApi.Validators;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddControllers()
    .AddNewtonsoftJson()
    .AddFluentValidation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración DbContext
builder.Services.AddDbContext<LibraryDbContext>(options => 
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 23))));

// Registro de validadores
builder.Services.AddValidatorsFromAssemblyContaining<DocumentCreateValidator>();

var app = builder.Build();

// Obtener un logger para depuración temprana
var startupLogger = app.Services.GetRequiredService<ILogger<Program>>();
startupLogger.LogInformation("Aplicación iniciándose. Entorno actual: {EnvironmentName}", app.Environment.EnvironmentName);

// Configuración del pipeline
if (app.Environment.IsDevelopment())
{
    startupLogger.LogInformation("Entorno de Development detectado. Configurando Swagger y Developer Exception Page.");
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "KuantumLibrary API V1");
        // Por defecto, Swagger UI se sirve en "/swagger".
        // Si quieres que Swagger UI se sirva en la raíz (http://localhost:5000/),
        // descomenta la siguiente línea:
        // options.RoutePrefix = string.Empty;
    });
    app.UseDeveloperExceptionPage();
}
else
{
    startupLogger.LogWarning("No se está en entorno de Development. Swagger UI y Developer Exception Page no se configurarán. Entorno: {EnvironmentName}", app.Environment.EnvironmentName);
}

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Aplicar migraciones
try
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
    dbContext.Database.Migrate();
}
catch (Exception ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Error al aplicar migraciones");
}

app.Run();