FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["MovieShopMVC.API/MovieShopMVC.API.csproj", "MovieShopMVC.API/"]
COPY ["MovieShop.ApplicationCore/MovieShop.ApplicationCore.csproj", "MovieShop.ApplicationCore/"]
COPY ["MovieShop.Infrastructure/MovieShop.Infrastructure.csproj", "MovieShop.Infrastructure/"]
RUN dotnet restore "MovieShopMVC.API/MovieShopMVC.API.csproj"
COPY . .
WORKDIR "/src/MovieShopMVC.API"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MovieShopMVC.API.dll"]
