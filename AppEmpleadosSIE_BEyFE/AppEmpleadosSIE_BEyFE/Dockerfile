COPY . .
RUN dotnet restore AppEmpleadosSIE_BEyFE.csproj

RUN dotnet publish AppEmpleadosSIE_BEyFE.csproj -c Release -o out

# Copy the .csproj file first to leverage Docker's layer caching
# This path is relative to the WORKDIR /app
COPY AppEmpleadosSIE_BEyFE/AppEmpleadosSIE_BEyFE.csproj ./

# Restore dependencies
RUN dotnet restore AppEmpleadosSIE_BEyFE.csproj

# Usa la imagen base de .NET 8.0 runtime para ejecutar
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expone el puerto 10000 (requerido por Render)
EXPOSE 10000

# Comando para iniciar la aplicación
ENTRYPOINT ["dotnet", "AppEmpleadosSIE_BEyFE.dll"]
