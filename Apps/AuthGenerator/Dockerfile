FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Apps/AuthGenerator/src/jwtApi/jwtApi.csproj", "Apps/AuthGenerator/src/jwtApi/"]
RUN dotnet restore "Apps/AuthGenerator/src/jwtApi/jwtApi.csproj"
COPY Apps/AuthGenerator/src/jwtApi/ Apps/AuthGenerator/src/jwtApi/
WORKDIR "Apps/AuthGenerator/src/jwtApi"
RUN dotnet build "./jwtApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./jwtApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "jwtApi.dll"]
