FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["fabarblog.csproj", "./"]
RUN dotnet restore

COPY . .

EXPOSE 80
EXPOSE 8080

ENTRYPOINT ["dotnet", "watch", "run", "--urls", "http://0.0.0.0:8080"]
