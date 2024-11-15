FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GuessingGameApiApplication/GuessingGame.API/GuessingGame.API.csproj", "GuessingGame.API/"]
COPY ["GuessingGameApiApplication/GuessingGame.Application/GuessingGame.Application.csproj", "GuessingGame.Application/"]
COPY ["GuessingGameApiApplication/GuessingGame.Domain/GuessingGame.Domain.csproj", "GuessingGame.Domain/"]
COPY ["GuessingGameApiApplication/GuessingGame.Infrastructure/GuessingGame.Infrastructure.csproj", "GuessingGame.Infrastructure/"]
RUN dotnet restore "GuessingGame.API/GuessingGame.API.csproj"
COPY . .
WORKDIR "/src/GuessingGameApiApplication/GuessingGame.API"
RUN dotnet build "GuessingGame.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GuessingGame.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GuessingGame.API.dll"]
