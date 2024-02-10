FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Presentation/Identity/HandmadeShop.Identity/HandmadeShop.Identity.csproj", "src/Presentation/Identity/HandmadeShop.Identity/"]
COPY ["src/Shared/HandmadeShop.Common/HandmadeShop.Common.csproj", "src/Shared/HandmadeShop.Common/"]
COPY ["src/Domain/HandmadeShop.Domain/HandmadeShop.Domain.csproj", "src/Domain/HandmadeShop.Domain/"]
COPY ["src/Infrastructure/HandmadeShop.Context/HandmadeShop.Context.csproj", "src/Infrastructure/HandmadeShop.Context/"]
COPY ["src/Services/HandmadeShop.Services.Settings/HandmadeShop.Services.Settings.csproj", "src/Services/HandmadeShop.Services.Settings/"]
RUN dotnet restore "src/Presentation/Identity/HandmadeShop.Identity/HandmadeShop.Identity.csproj"
COPY . .
WORKDIR "/src/src/Presentation/Identity/HandmadeShop.Identity"
RUN dotnet build "HandmadeShop.Identity.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HandmadeShop.Identity.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HandmadeShop.Identity.dll"]
