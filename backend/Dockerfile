FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY src/ .

RUN dotnet restore "SistemaControle.Api/SistemaControle.Api/SistemaControle.Api.csproj"
RUN dotnet publish "SistemaControle.Api/SistemaControle.Api/SistemaControle.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:10000

ENTRYPOINT ["dotnet", "SistemaControle.Api.dll"]