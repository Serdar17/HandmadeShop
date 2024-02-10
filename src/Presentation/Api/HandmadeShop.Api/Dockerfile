FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Presentation/Api/HandmadeShop.Api/HandmadeShop.Api.csproj", "src/Presentation/Api/HandmadeShop.Api/"]
COPY ["src/Infrastructure/HandmadeShop.Context.Seeder/HandmadeShop.Context.Seeder.csproj", "src/Infrastructure/HandmadeShop.Context.Seeder/"]
COPY ["src/Infrastructure/HandmadeShop.Context/HandmadeShop.Context.csproj", "src/Infrastructure/HandmadeShop.Context/"]
COPY ["src/Domain/HandmadeShop.Domain/HandmadeShop.Domain.csproj", "src/Domain/HandmadeShop.Domain/"]
COPY ["src/Shared/HandmadeShop.Common/HandmadeShop.Common.csproj", "src/Shared/HandmadeShop.Common/"]
COPY ["src/Infrastructure/Migrations/HandmadeShop.Context.Migrations.MSSQL/HandmadeShop.Context.Migrations.MSSQL.csproj", "src/Infrastructure/Migrations/HandmadeShop.Context.Migrations.MSSQL/"]
COPY ["src/Infrastructure/Migrations/HandmadeShop.Context.Migrations.MySql/HandmadeShop.Context.Migrations.MySql.csproj", "src/Infrastructure/Migrations/HandmadeShop.Context.Migrations.MySql/"]
COPY ["src/Infrastructure/Migrations/HandmadeShop.Context.Migrations.PgSql/HandmadeShop.Context.Migrations.PgSql.csproj", "src/Infrastructure/Migrations/HandmadeShop.Context.Migrations.PgSql/"]
COPY ["src/Services/HandmadeShop.Services.Settings/HandmadeShop.Services.Settings.csproj", "src/Services/HandmadeShop.Services.Settings/"]
COPY ["src/Services/HandmadeShop.Services.Logger/HandmadeShop.Services.Logger.csproj", "src/Services/HandmadeShop.Services.Logger/"]
COPY ["src/Services/HandmadeShop.Services.RabbitMq/HandmadeShop.Services.RabbitMq.csproj", "src/Services/HandmadeShop.Services.RabbitMq/"]
COPY ["src/Application/HandmadeShop.Application/HandmadeShop.Application.csproj", "src/Application/HandmadeShop.Application/"]
COPY ["src/Application/HandmadeShop.UseCase.Auth/HandmadeShop.UseCase.Auth.csproj", "src/Application/HandmadeShop.UseCase.Auth/"]
RUN dotnet restore "src/Presentation/Api/HandmadeShop.Api/HandmadeShop.Api.csproj"
COPY . .
WORKDIR "/src/src/Presentation/Api/HandmadeShop.Api"
RUN dotnet build "HandmadeShop.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HandmadeShop.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HandmadeShop.Api.dll"]
