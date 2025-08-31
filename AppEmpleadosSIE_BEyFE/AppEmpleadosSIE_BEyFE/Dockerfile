# Use an SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the .csproj file first to leverage Docker's layer caching
# This path is relative to the WORKDIR /app
COPY AppEmpleadosSIE_BEyFE/AppEmpleadosSIE_BEyFE.csproj ./

# Restore dependencies
RUN dotnet restore AppEmpleadosSIE_BEyFE.csproj

# Copy the rest of the source code and publish the application
COPY . .
RUN dotnet publish AppEmpleadosSIE_BEyFE/AppEmpleadosSIE_BEyFE.csproj -c Release -o /app/out

# Use a lighter ASP.NET runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 10000
ENTRYPOINT ["dotnet", "AppEmpleadosSIE_BEyFE.dll"]
