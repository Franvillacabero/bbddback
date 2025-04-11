#!/bin/bash

# Iniciar la aplicación .NET en segundo plano
dotnet /app/back.dll --urls "http://localhost:5006" &

# Esperar un momento para que la aplicación inicie
sleep 5

# Iniciar Nginx en primer plano (para que el contenedor no se detenga)
nginx -g "daemon off;"