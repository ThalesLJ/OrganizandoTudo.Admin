# Expondo a porta 8080 para o host
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080 

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

# Define a variable for the port
ARG PORT=5000
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}

# Expose the port
EXPOSE ${PORT}

# Define o entrypoint para o Kestrel
ENTRYPOINT ["dotnet", "OrganizandoTudo.Admin.dll"]
