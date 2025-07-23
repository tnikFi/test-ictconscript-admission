FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY . .
RUN dotnet restore "Logbook.sln"
RUN dotnet build "Logbook.sln" -c $BUILD_CONFIGURATION -o /app/build --no-restore
RUN dotnet test "Logbook.sln" -c $BUILD_CONFIGURATION --verbosity detailed

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Logbook.sln" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]
