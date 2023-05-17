﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 81
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RadonAPI/RadonAPI.csproj", "RadonAPI/"]
RUN dotnet restore "RadonAPI/RadonAPI.csproj"
COPY . .
WORKDIR "/src/RadonAPI"
RUN dotnet build "RadonAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RadonAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RadonAPI.dll"]