FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Learn.Ddd.TaskTracker.Api/Learn.Ddd.TaskTracker.Api.csproj", "src/Learn.Ddd.TaskTracker.Api/"]
COPY ["src/Learn.Ddd.TaskTracker.Core/Learn.Ddd.TaskTracker.Core.csproj", "src/Learn.Ddd.TaskTracker.Core/"]
COPY ["src/Learn.Ddd.TaskTracker.Infrastructure/Learn.Ddd.TaskTracker.Infrastructure.csproj", "src/Learn.Ddd.TaskTracker.Infrastructure/"]
RUN dotnet restore "src/Learn.Ddd.TaskTracker.Api/Learn.Ddd.TaskTracker.Api.csproj"
COPY . .
WORKDIR "/src/src/Learn.Ddd.TaskTracker.Api"
RUN dotnet build "Learn.Ddd.TaskTracker.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Learn.Ddd.TaskTracker.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Learn.Ddd.TaskTracker.Api.dll"]
