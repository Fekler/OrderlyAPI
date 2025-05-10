FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/API/SalesOrderManagement.API/SalesOrderManagement.API.csproj", "src/API/SalesOrderManagement.API/"]
COPY ["src/API/SalesOrderManagement.API.IOC/SalesOrderManagement.API.IOC.csproj", "src/API/SalesOrderManagement.API.IOC/"]
COPY ["src/API/SalesOrderManagement.API.Infra/SalesOrderManagement.API.Infra.csproj", "src/API/SalesOrderManagement.API.Infra/"]
COPY ["src/SalesOrderManagement.Application/SalesOrderManagement.Application.csproj", "src/SalesOrderManagement.Application/"]
COPY ["src/SalesOrderManagement.Domain/SalesOrderManagement.Domain.csproj", "src/SalesOrderManagement.Domain/"]
COPY ["src/SharedKernel/SharedKernel.csproj", "src/SharedKernel/"]
RUN dotnet restore "./src/API/SalesOrderManagement.API/SalesOrderManagement.API.csproj"
COPY . .
WORKDIR "/src/src/API/SalesOrderManagement.API"
RUN dotnet build "./SalesOrderManagement.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SalesOrderManagement.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SalesOrderManagement.API.dll"]