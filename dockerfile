# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "back.csproj"
RUN dotnet publish "back.csproj" -c Release -o /app/publish

# Etapa 2: Runtime con Nginx
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Instalar Nginx
RUN apt-get update && apt-get install -y nginx

# Copiar la aplicación .NET
WORKDIR /app
COPY --from=build /app/publish .

# Crear directorios necesarios
RUN mkdir -p /etc/nginx/ssl

# Copiar configuración de Nginx y certificados
COPY nginx.conf /etc/nginx/nginx.conf
COPY ssl/server.crt /etc/nginx/ssl/server.crt
COPY ssl/server.key /etc/nginx/ssl/server.key

# Establecer permisos adecuados para los certificados
RUN chmod 600 /etc/nginx/ssl/server.key
RUN chmod 644 /etc/nginx/ssl/server.crt

# Script de inicio
COPY start.sh /start.sh
RUN chmod +x /start.sh

# Exponer puertos
EXPOSE 443
EXPOSE 5006

# Iniciar aplicación y Nginx
ENTRYPOINT ["/start.sh"]