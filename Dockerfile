FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OrganizandoTudo.Admin/OrganizandoTudo.Admin.csproj", "OrganizandoTudo.Admin/"]
RUN dotnet restore "./OrganizandoTudo.Admin/OrganizandoTudo.Admin.csproj"
COPY . .
WORKDIR "/src/OrganizandoTudo.Admin"
RUN dotnet build "./OrganizandoTudo.Admin.csproj" -c $BUILD_CONFIGURATION -o /app/build

ARG PORT=5000
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OrganizandoTudo.Admin.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrganizandoTudo.Admin.dll"]