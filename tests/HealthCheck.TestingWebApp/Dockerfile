FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["tests/HealthCheck.TestingWebApp/HealthCheck.TestingWebApp.csproj", "tests/HealthCheck.TestingWebApp/"]
RUN dotnet restore "tests/HealthCheck.TestingWebApp/HealthCheck.TestingWebApp.csproj"
COPY . .
WORKDIR "/src/tests/HealthCheck.TestingWebApp"
RUN dotnet build "HealthCheck.TestingWebApp.csproj" -c Release -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "HealthCheck.TestingWebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HealthCheck.TestingWebApp.dll"]
