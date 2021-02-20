FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /src
COPY ["/ElasticSearchExample/ElasticSearchExample.csproj", "ElasticSearchExample/"]
COPY ["/Search.Core/Search.Core.csproj", "Search.Core/"]
COPY ["/MessageBus.Core/MessageBus.Core.csproj", "MessageBus.Core/"]
RUN dotnet restore "ElasticSearchExample/ElasticSearchExample.csproj"

COPY . .
WORKDIR "/src/ElasticSearchExample"
RUN dotnet build "ElasticSearchExample.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app
FROM build AS publish
RUN dotnet publish "ElasticSearchExample.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ElasticSearchExample.dll"]