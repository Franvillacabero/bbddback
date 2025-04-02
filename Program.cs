using Back.Controllers;
using Back.Repository;
using Back.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BBDD");

// Repositorios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>(provider =>
    new UsuarioRepository(connectionString));

// Servicios
builder.Services.AddScoped<IUsuarioService, UsuarioService>(provider =>
    new UsuarioService(provider.GetRequiredService<IUsuarioRepository>()));


var AllowAll = "_AllowAll";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_AllowAll",
        policy =>
        {
                  policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Add services to the container.
builder.Services.AddControllers();

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuración del pipeline HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(AllowAll);      
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "La API está en ejecución correctamente.");

app.Run();
