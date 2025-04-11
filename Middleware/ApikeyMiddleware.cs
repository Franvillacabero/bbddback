using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Back.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string ApiKeyValue = "your-secret-key-123";

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Permitir la ruta de login sin restricciones
            if (context.Request.Path.Value.Contains("/api/Usuario/login"))
            {
                await _next(context);
                return;
            }

            // Para todas las demás rutas de API, verificar Referer
            string referer = context.Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer))
            {
                await _next(context);
                return;
            }

            // Si no tiene Referer, rechazar
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Acceso no autorizado");
            return;
        }
    }
}