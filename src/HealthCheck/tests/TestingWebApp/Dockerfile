#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["src/HealthCheck/tests/TestingWebApp/HealthCheck.TestingWebApp.csproj", "src/HealthCheck/tests/TestingWebApp/"]
COPY ["src/HealthCheck/src/Console/HealthCheck.Console.csproj", "src/HealthCheck/src/Console/"]
RUN dotnet restore "src/HealthCheck/tests/TestingWebApp/HealthCheck.TestingWebApp.csproj"
COPY . .
WORKDIR "/src/src/HealthCheck/tests/TestingWebApp"
RUN dotnet build "HealthCheck.TestingWebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HealthCheck.TestingWebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HealthCheck.TestingWebApp.dll"]