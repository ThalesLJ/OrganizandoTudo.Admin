# Usar a imagem base do .NET 8 SDK
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Copiar arquivos de configuração e binários
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["OrganizandoTudo.Admin/OrganizandoTudo.Admin.csproj", "OrganizandoTudo.Admin/"]
RUN dotnet restore "OrganizandoTudo.Admin/OrganizandoTudo.Admin.csproj"
COPY . .
WORKDIR "/src/OrganizandoTudo.Admin"
RUN dotnet build "OrganizandoTudo.Admin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrganizandoTudo.Admin.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrganizandoTudo.Admin.dll"]
