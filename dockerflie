# Use a imagem base do SDK do .NET para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copia o arquivo .csproj e restaura as dependências
COPY *.csproj ./
RUN dotnet restore

# Copia o restante dos arquivos e builda a aplicação
COPY . ./
RUN dotnet publish -c Release -o out

# Use a imagem runtime do .NET para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Define o entrypoint para o Kestrel
ENTRYPOINT ["dotnet", "OrganizandoTudo.Admin.dll"]
