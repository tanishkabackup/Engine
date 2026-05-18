FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src


COPY ["backend/TaskInsightEngine/TaskInsightEngine.Api/TaskInsightEngine.Api.csproj", "TaskInsightEngine.Api/"]
COPY ["backend/TaskInsightEngine/TaskInsightEngine.Application/TaskInsightEngine.Application.csproj", "TaskInsightEngine.Application/"]
COPY ["backend/TaskInsightEngine/TaskInsightEngine.Infrastructure/TaskInsightEngine.Infrastructure.csproj", "TaskInsightEngine.Infrastructure/"]
COPY ["backend/TaskInsightEngine/TaskInsightEngine.Domain/TaskInsightEngine.Domain.csproj", "TaskInsightEngine.Domain/"]

# Restore dependencies
RUN dotnet restore "TaskInsightEngine.Api/TaskInsightEngine.Api.csproj"


COPY . .


RUN dotnet build "backend/TaskInsightEngine/TaskInsightEngine.Api/TaskInsightEngine.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "backend/TaskInsightEngine/TaskInsightEngine.Api/TaskInsightEngine.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskInsightEngine.Api.dll"]