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
RUN mkdir -p /ssl

# Copiar configuración de Nginx y certificados
COPY nginx.conf /nginx.conf
COPY ssl/server.crt /ssl/server.crt
COPY ssl/server.key /ssl/server.key

# Establecer permisos adecuados para los certificados
RUN chmod 600 /ssl/server.key
RUN chmod 644 /ssl/server.crt

# Script de inicio
COPY start.sh /start.sh
RUN chmod +x /start.sh

# Exponer puertos
EXPOSE 443
EXPOSE 5006

# Iniciar aplicación y Nginx
ENTRYPOINT ["/start.sh"]