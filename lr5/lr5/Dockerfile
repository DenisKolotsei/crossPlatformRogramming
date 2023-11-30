FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Копирование и восстановление зависимостей
COPY *.csproj .
RUN dotnet restore

# Копирование остальных файлов и сборка
COPY . .
RUN dotnet publish -c Release -o out

# Образ для запуска
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "lr5.dll"]