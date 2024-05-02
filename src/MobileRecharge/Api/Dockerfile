#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/MobileRecharge/Api/MobileRecharge.Api.csproj", "src/MobileRecharge/Api/"]
COPY ["src/MobileRecharge/Core/MobileRecharge.Core.csproj", "src/MobileRecharge/Core/"]
COPY ["src/Shared/Infrastructure/OnlineWallet.Infrastructure.csproj", "src/Shared/Infrastructure/"]
COPY ["src/Shared/Domain/OnlineWallet.Domain.csproj", "src/Shared/Domain/"]
RUN dotnet restore "./src/MobileRecharge/Api/MobileRecharge.Api.csproj"
COPY . .
WORKDIR "/src/src/MobileRecharge/Api"
RUN dotnet build "./MobileRecharge.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MobileRecharge.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MobileRecharge.Api.dll"]