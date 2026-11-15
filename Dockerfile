# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["wt_lab7_7postgres_efcore_lyashuk.csproj", "./"]
RUN dotnet restore "wt_lab7_7postgres_efcore_lyashuk.csproj"
COPY . .
RUN dotnet build "wt_lab7_7postgres_efcore_lyashuk.csproj" -c Release -o /app/build

# Этап публикации
FROM build AS publish
RUN dotnet publish "wt_lab7_7postgres_efcore_lyashuk.csproj" -c Release -o /app/publish

# Финальный образ для запуска
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 10000
ENTRYPOINT ["dotnet", "wt_lab7_7postgres_efcore_lyashuk.dll"]