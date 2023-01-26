#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

COPY . /app

WORKDIR /app

RUN dotnet restore "elefanti60.csproj"

RUN dotnet build "elefanti60.csproj" -c Release -o /app/build

RUN dotnet publish "elefanti60.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "elefanti60.dll"]