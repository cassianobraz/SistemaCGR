FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore ./src/SistemaControle.Api/SistemaControle.Api/SistemaControle.Api.csproj
RUN dotnet publish ./src/SistemaControle.Api/SistemaControle.Api/SistemaControle.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish ./
ENTRYPOINT ["dotnet", "SistemaControle.Api.dll"]
