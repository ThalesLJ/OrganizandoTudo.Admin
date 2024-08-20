FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Production

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OrganizandoTudo.Admin\OrganizandoTudo.Admin.csproj", "OrganizandoTudo.Admin/"]
COPY . .
WORKDIR "/src/OrganizandoTudo.Admin"
RUN dotnet build "./OrganizandoTudo.Admin.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OrganizandoTudo.Admin.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

ARG PORT=5000
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}

EXPOSE ${PORT}

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrganizandoTudo.Admin.dll"]