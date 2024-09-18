# Fase de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Instalar as ferramentas necessárias, incluindo dotnet-ef
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Copiar o csproj e restaurar dependências
COPY ["fabarblog.csproj", "./"]
RUN dotnet restore

# Copiar o restante do código e construir o projeto
COPY . .
RUN dotnet build -c Debug -o /app/build

# Fase final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Expor as portas necessárias
EXPOSE 80
EXPOSE 8080

# Copiar os arquivos da fase de build
COPY --from=build /app/build .

# Rodar o dotnet watch para hot reload

ENTRYPOINT ["dotnet", "fabarblog.dll"]