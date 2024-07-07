# Use the official .NET Core runtime as a base image
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

# Use the official .NET SDK as a build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy and restore project dependencies
COPY ["ChatsControllerTests/UnitTestsController.csproj", "ChatsControllerTests/"]
COPY ["ShuttleX_task_api/ShuttleX_task_api.csproj", "ShuttleX_task_api/"]
RUN dotnet restore "./ChatsControllerTests/UnitTestsController.csproj"

# Copy the rest of the application code
COPY . .
WORKDIR "/src/ChatsControllerTests"
RUN dotnet build "./UnitTestsController.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UnitTestsController.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage: Copy the build output to the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UnitTestsController.dll"]
