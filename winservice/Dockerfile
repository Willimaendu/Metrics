#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0-alpine AS base
WORKDIR /app
EXPOSE 8001

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /src
COPY ["winservice.csproj", "."]
RUN dotnet restore "./winservice.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "winservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "winservice.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "winservice.dll"]