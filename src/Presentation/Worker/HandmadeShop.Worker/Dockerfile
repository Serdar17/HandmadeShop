FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Presentation/Worker/HandmadeShop.Worker/HandmadeShop.Worker.csproj", "src/Presentation/Worker/HandmadeShop.Worker/"]
COPY ["src/Services/HandmadeShop.Services.EmailSender/HandmadeShop.Services.EmailSender.csproj", "src/Services/HandmadeShop.Services.EmailSender/"]
COPY ["src/Application/HandmadeShop.Application/HandmadeShop.Application.csproj", "src/Application/HandmadeShop.Application/"]
COPY ["src/Domain/HandmadeShop.Domain/HandmadeShop.Domain.csproj", "src/Domain/HandmadeShop.Domain/"]
COPY ["src/Domain/HandmadeShop.Infrastructure.Abstractions/HandmadeShop.Infrastructure.Abstractions.csproj", "src/Domain/HandmadeShop.Infrastructure.Abstractions/"]
COPY ["src/Shared/HandmadeShop.Common/HandmadeShop.Common.csproj", "src/Shared/HandmadeShop.Common/"]
COPY ["src/Shared/HandmadeShop.SharedModel/HandmadeShop.SharedModel.csproj", "src/Shared/HandmadeShop.SharedModel/"]
COPY ["src/Services/HandmadeShop.Services.Settings/HandmadeShop.Services.Settings.csproj", "src/Services/HandmadeShop.Services.Settings/"]
COPY ["src/Services/HandmadeShop.Services.Logger/HandmadeShop.Services.Logger.csproj", "src/Services/HandmadeShop.Services.Logger/"]
COPY ["src/Services/HandmadeShop.Services.RabbitMq/HandmadeShop.Services.RabbitMq.csproj", "src/Services/HandmadeShop.Services.RabbitMq/"]
RUN dotnet restore "src/Presentation/Worker/HandmadeShop.Worker/HandmadeShop.Worker.csproj"
COPY . .
WORKDIR "/src/src/Presentation/Worker/HandmadeShop.Worker"
RUN dotnet build "HandmadeShop.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HandmadeShop.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HandmadeShop.Worker.dll"]
