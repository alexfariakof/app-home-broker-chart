FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .

WORKDIR /src/HomeBrokerSPA
RUN dotnet build --restore -c Release -o /app/build

FROM build AS publish
WORKDIR /src/HomeBrokerSPA
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

WORKDIR /src/HomeBrokerSPA/HomeBrokerChart
RUN apt-get update && \
    apt-get install -y curl && \
    curl -sL https://deb.nodesource.com/setup_18.x | bash - && \
    apt-get install -y nodejs
RUN npm install -g npm@10.2.5 && \
    npm install -g @angular/core@15 @angular/cli@15 && \
    npm install 
RUN npm run build -- --configuration production

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "HomeBrokerSPA.dll"]
