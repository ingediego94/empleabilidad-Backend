FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_ENVIRONMENT=Development
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["cursos.Api/cursos.Api.csproj", "cursos.Api/"]
COPY ["cursos.Application/cursos.Application.csproj", "cursos.Application/"]
COPY ["cursos.Domain/cursos.Domain.csproj", "cursos.Domain/"]
COPY ["cursos.Infrastructure/cursos.Infrastructure.csproj", "cursos.Infrastructure/"]
RUN dotnet restore "cursos.Api/cursos.Api.csproj"
COPY . .
WORKDIR "/src/cursos.Api"
RUN dotnet build "./cursos.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./cursos.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "cursos.Api.dll"]
