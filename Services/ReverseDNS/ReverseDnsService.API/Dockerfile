#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/ReverseDNS/ReverseDnsService.API/ReverseDnsService.API.csproj", "Services/ReverseDNS/ReverseDnsService.API/"]
RUN dotnet restore "Services/ReverseDNS/ReverseDnsService.API/ReverseDnsService.API.csproj"
COPY . .
WORKDIR "/src/Services/ReverseDNS/ReverseDnsService.API"
RUN dotnet build "ReverseDnsService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReverseDnsService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReverseDnsService.API.dll"]