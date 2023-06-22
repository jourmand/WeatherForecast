#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Endpoints/WeatherForecast.Endpoints.WebApi/WeatherForecast.Endpoints.WebApi.csproj", "src/Endpoints/WeatherForecast.Endpoints.WebApi/"]
COPY ["src/Core/WeatherForecast.Core.ApplicationService/WeatherForecast.Core.ApplicationService.csproj", "src/Core/WeatherForecast.Core.ApplicationService/"]
COPY ["src/Core/WeatherForecast.Core.Domain/WeatherForecast.Core.Domain.csproj", "src/Core/WeatherForecast.Core.Domain/"]
COPY ["src/Infrastructures/WeatherForecast.Infrastructures.Data/WeatherForecast.Infrastructures.Data.csproj", "src/Infrastructures/WeatherForecast.Infrastructures.Data/"]
RUN dotnet restore "src/Endpoints/WeatherForecast.Endpoints.WebApi/WeatherForecast.Endpoints.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Endpoints/WeatherForecast.Endpoints.WebApi"
RUN dotnet build "WeatherForecast.Endpoints.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WeatherForecast.Endpoints.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeatherForecast.Endpoints.WebApi.dll"]