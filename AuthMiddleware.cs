using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Net;

namespace Back.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _validToken;
        private readonly List<string> _allowedIPs = new List<string> 
        { 
            "152.228.135.50"
        };

        public AuthMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _validToken = config["SecretToken"];
        }

        public async Task InvokeAsync(HttpContext context)
        {
            bool isAuthenticated = false;
            
            // Verificar IP
            string ipAddress = context.Connection.RemoteIpAddress?.ToString();
            if (_allowedIPs.Contains(ipAddress))
            {
                isAuthenticated = true;
            }

            // Mantener validación de token como respaldo
            if (context.Request.Cookies.TryGetValue("auth_token", out var token))
            {
                if (token == _validToken)
                {
                    isAuthenticated = true;
                }
            }

            if (isAuthenticated)
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Acceso no autorizado");
            }
        }
    }
}