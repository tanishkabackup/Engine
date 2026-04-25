FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files first to leverage Docker caching
COPY ["TaskInsightEngine.Api/TaskInsightEngine.Api.csproj", "TaskInsightEngine.Api/"]
COPY ["TaskInsightEngine.Application/TaskInsightEngine.Application.csproj", "TaskInsightEngine.Application/"]
COPY ["TaskInsightEngine.Infrastructure/TaskInsightEngine.Infrastructure.csproj", "TaskInsightEngine.Infrastructure/"]
COPY ["TaskInsightEngine.Domain/TaskInsightEngine.Domain.csproj", "TaskInsightEngine.Domain/"]

# Restore dependencies
RUN dotnet restore "TaskInsightEngine.Api/TaskInsightEngine.Api.csproj"

# Copy EVERYTHING else
COPY . .

# Build from the root using the full path to the project
RUN dotnet build "TaskInsightEngine.Api/TaskInsightEngine.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskInsightEngine.Api/TaskInsightEngine.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskInsightEngine.Api.dll"]