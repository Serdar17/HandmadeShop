FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Presentation/Web/HandmadeShop.Web/HandmadeShop.Web.csproj", "src/Presentation/Web/HandmadeShop.Web/"]
COPY ["src/Domain/HandmadeShop.Domain/HandmadeShop.Domain.csproj", "src/Domain/HandmadeShop.Domain/"]
COPY ["src/Shared/HandmadeShop.SharedModel/HandmadeShop.SharedModel.csproj", "src/Shared/HandmadeShop.SharedModel/"]
RUN dotnet restore "src/Presentation/Web/HandmadeShop.Web/HandmadeShop.Web.csproj"
COPY . .
WORKDIR "/src/src/Presentation/Web/HandmadeShop.Web"
RUN dotnet build "HandmadeShop.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HandmadeShop.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM nginx:alpine
WORKDIR /var/www/web

COPY --from=publish /app/publish/wwwroot .
COPY src/Presentation/Web/HandmadeShop.Web/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80