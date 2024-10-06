FROM mcr.microsoft.com/dotnet/aspnet:8.0 as base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM alexfariakof/dotnet-sdk-8.0-with-node AS publish
WORKDIR /src
COPY . .

WORKDIR /src/HomeBrokerSPA
RUN dotnet build --restore -c Release -o /app/build
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:8.0 as final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "HomeBroker.SPA.dll"]
