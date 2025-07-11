# 1. Сборка приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем csproj файлы и восстанавливаем зависимости отдельно (для кэширования)
COPY *.sln .
COPY CryptoApp.API/*.csproj ./CryptoApp.API/
COPY CryptoApp.Application/*.csproj ./CryptoApp.Application/
COPY CryptoApp.DataAccess/*.csproj ./CryptoApp.DataAccess/
COPY CryptoApp.Core/*.csproj ./CryptoApp.Core/
COPY CryptoApp.Tests/*.csproj ./CryptoApp.Tests/

RUN dotnet restore "CryptoApp.API.sln"

# Копируем остальной исходный код
COPY . .

# Публикуем основной проект
RUN dotnet publish "CryptoApp.API/CryptoApp.API.csproj" -c Release -o /app/publish

# 2. Финальный образ для запуска
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Открываем порт (если требуется, для Render и других)
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENV JwtOptions__SecretKey=cryptoappWnRpptcweQusavbmaoWismxcryptoappWnRpptcweQusavbmaoWismx
ENV JwtOptions__ExpiresHours=1

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "CryptoApp.API.dll"]
