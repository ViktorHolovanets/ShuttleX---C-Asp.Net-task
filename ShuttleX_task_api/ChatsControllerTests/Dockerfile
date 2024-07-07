#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ChatsControllerTests/UnitTestsController.csproj", "ChatsControllerTests/"]
COPY ["ShuttleX_task_api/ShuttleX_task_api.csproj", "ShuttleX_task_api/"]
RUN dotnet restore "./ChatsControllerTests/UnitTestsController.csproj"
COPY . .
WORKDIR "/src/ChatsControllerTests"
RUN dotnet build "./UnitTestsController.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UnitTestsController.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UnitTestsController.dll"]