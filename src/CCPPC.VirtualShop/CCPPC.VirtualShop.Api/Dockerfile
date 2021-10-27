#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CCPPC.VirtualShop.Api/CCPPC.VirtualShop.Api.csproj", "CCPPC.VirtualShop.Api/"]
RUN dotnet restore "CCPPC.VirtualShop.Api/CCPPC.VirtualShop.Api.csproj"
COPY . .
WORKDIR "/src/CCPPC.VirtualShop.Api"
RUN dotnet build "CCPPC.VirtualShop.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CCPPC.VirtualShop.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CCPPC.VirtualShop.Api.dll"]