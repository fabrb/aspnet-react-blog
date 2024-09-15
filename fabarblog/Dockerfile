FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["fabarblog.csproj", "./"]
RUN dotnet restore

COPY . .
RUN dotnet build -c Debug -o /app/build

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Imagem final
FROM base AS final
WORKDIR /app
COPY --from=build /app/build .

ENTRYPOINT ["dotnet", "fabarblog.dll"]
