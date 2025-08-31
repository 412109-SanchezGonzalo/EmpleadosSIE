FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app
COPY . .
RUN dotnet restore AppEmpleadosSIE_BEyFE/AppEmpleadosSIE_BEyFE/AppEmpleadosSIE_BEyFE.csproj
RUN dotnet publish AppEmpleadosSIE_BEyFE/AppEmpleadosSIE_BEyFE/AppEmpleadosSIE_BEyFE.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 10000
ENTRYPOINT ["dotnet", "AppEmpleadosSIE_BEyFE.dll"]
