#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/GeoIP/GeoIPService/GeoIPService.API.csproj", "Services/GeoIP/GeoIPService.API/"]
RUN dotnet restore "Services/GeoIP/GeoIPService/GeoIPService.API.csproj"
COPY . .
WORKDIR "/src/Services/GeoIP/GeoIPService.API"
RUN dotnet build "GeoIPService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GeoIPService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeoIPService.API.dll"]