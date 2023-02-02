#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.hamdocker.ir/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.hamdocker.ir/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/BookShop.Web/BookShop.Web.csproj", "src/BookShop.Web/"]
COPY ["src/BookShop/BookShop.csproj", "src/BookShop/"]
RUN dotnet restore "src/BookShop.Web/BookShop.Web.csproj"
COPY . .
WORKDIR "/src/src/BookShop.Web"
RUN dotnet build "BookShop.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookShop.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookShop.Web.dll"]