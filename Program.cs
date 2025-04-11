using Back.Controllers;
using Back.Repository;
using Back.Services;
using System.Net;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BBDD");

// Repositorios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>(provider =>
    new UsuarioRepository(connectionString));

builder.Services.AddScoped<IRegistroRepository, RegistroRepository>(provider =>
    new RegistroRepository(connectionString));

builder.Services.AddScoped<IClienteRepository, ClienteRepository>(provider =>
    new ClienteRepository(connectionString));

builder.Services.AddScoped<ITipoServicioRepository, TipoServicioRepository>(provider =>
    new TipoServicioRepository(connectionString));

// Servicios
builder.Services.AddScoped<IUsuarioService, UsuarioService>(provider =>
    new UsuarioService(provider.GetRequiredService<IUsuarioRepository>()));

builder.Services.AddScoped<IRegistroService, RegistroService>(provider =>
    new RegistroService(provider.GetRequiredService<IRegistroRepository>()));

builder.Services.AddScoped<IClienteService, ClienteService>(provider =>
    new ClienteService(provider.GetRequiredService<IClienteRepository>()));

builder.Services.AddScoped<ITipoServicioService, TipoServicioService>(provider =>
    new TipoServicioService(provider.GetRequiredService<ITipoServicioRepository>()));

// Configuración de CORS
var AllowAll = "_AllowAll";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowAll,
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Configuración de Kestrel para HTTPS
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Listen(IPAddress.Any, 5006, listenOptions =>
    {
        // Para desarrollo con certificado autofirmado
        // listenOptions.UseHttps();
        
        // Para producción, especificar el certificado
        // listenOptions.UseHttps("/path/to/certificate.pfx", "password");
    });
});

// Añadir servicios al contenedor
builder.Services.AddControllers();

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de encabezados para proxy inverso
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

var app = builder.Build();

// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Usar encabezados forwarded
app.UseForwardedHeaders();

// Redirección HTTPS
app.UseHttpsRedirection();

// Configuración de CORS
app.UseCors(AllowAll);

// Autorización
app.UseAuthorization();

// Mapeo de controladores
app.MapControllers();

// Endpoint de health check
app.MapGet("/", () => "La API está en ejecución correctamente.");

// Ejecutar la aplicación
app.Run();