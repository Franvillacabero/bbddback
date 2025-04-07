# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "back.csproj"
RUN dotnet publish "back.csproj" -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Exponer el puerto configurado en Program.cs
EXPOSE 5006
ENV ASPNETCORE_URLS=http://+:5006
ENTRYPOINT ["dotnet", "back.dll"]
