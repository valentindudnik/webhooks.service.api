#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Webhooks.Service.Api.Host/Webhooks.Service.Api.Host.csproj", "Webhooks.Service.Api.Host/"]
COPY ["Webhooks.Service.Models/Webhooks.Service.Models.csproj", "Webhooks.Service.Models/"]
COPY ["Webhooks.Service.Enums/Webhooks.Service.Enums.csproj", "Webhooks.Service.Enums/"]
COPY ["Webhooks.Service.Services/Webhooks.Service.Services.csproj", "Webhooks.Service.Services/"]
COPY ["Webhooks.Service.DataAccess/Webhooks.Service.DataAccess.csproj", "Webhooks.Service.DataAccess/"]
COPY ["Webhooks.Service.DataAccess.Models/Webhooks.Service.DataAccess.Models.csproj", "Webhooks.Service.DataAccess.Models/"]
COPY ["Webhooks.Service.Infrastructure/Webhooks.Service.Infrastructure.csproj", "Webhooks.Service.Infrastructure/"]
RUN dotnet restore "Webhooks.Service.Api.Host/Webhooks.Service.Api.Host.csproj"
COPY . .
WORKDIR "/src/Webhooks.Service.Api.Host"
RUN dotnet build "Webhooks.Service.Api.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Webhooks.Service.Api.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Webhooks.Service.Api.Host.dll"]