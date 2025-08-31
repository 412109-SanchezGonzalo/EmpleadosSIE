# Dockerfile para aplicación EmpleadosSIE .NET 8
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Crear usuario no-root para seguridad
RUN addgroup --system --gid 1001 dotnetuser
RUN adduser --system --uid 1001 --ingroup dotnetuser dotnetuser

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivo de proyecto desde la estructura correcta
COPY ["AppEmpleadosSIE_BEyFE/AppEmpleadosSIE_BEyFE/AppEmpleadosSIE_BEyFE.csproj", "AppEmpleadosSIE_BEyFE/AppEmpleadosSIE_BEyFE/"]

# Restaurar dependencias
RUN dotnet restore "AppEmpleadosSIE_BEyFE/AppEmpleadosSIE_BEyFE/AppEmpleadosSIE_BEyFE.csproj"

# Copiar todo el código fuente
COPY . .

# Build de la aplicación
WORKDIR "/src/AppEmpleadosSIE_BEyFE/AppEmpleadosSIE_BEyFE"
RUN dotnet build "AppEmpleadosSIE_BEyFE.csproj" -c Release -o /app/build

# Publicar la aplicación
FROM build AS publish
RUN dotnet publish "AppEmpleadosSIE_BEyFE.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa final
FROM base AS final
WORKDIR /app

# Copiar archivos publicados
COPY --from=publish /app/publish .

# Cambiar al usuario no-root
USER dotnetuser

# Configurar variables de entorno
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "AppEmpleadosSIE_BEyFE.dll"]