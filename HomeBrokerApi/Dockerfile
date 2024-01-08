#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .

WORKDIR "/src/HomeBrokerApi"
RUN apt-get update && \
    apt-get install -y curl && \
    curl -sL https://deb.nodesource.com/setup_18.x | bash - && \
    apt-get install -y nodejs
RUN npm install -g npm@10.2.5 && \
    npm install -g @angular/core@15 @angular/cli@15

RUN dotnet build "HomeBrokerApi.csproj" --restore -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HomeBrokerApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HomeBrokerApi.dll"]