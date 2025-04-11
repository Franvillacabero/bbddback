using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Back.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _validToken;

        public AuthMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _validToken = config["SecretToken"];
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Si hay un cookie de autenticación o el origen es correcto, permitir
            bool isAuthenticated = false;
            
            // Comprobar si viene de tu origen (referer)
            string referer = context.Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer) && referer.StartsWith("https://tu-frontend.com"))
            {
                isAuthenticated = true;
            }

            // O comprobar si tiene una cookie/token específico
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