# 1. Aşama: Derleme aşaması (Build Stage)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 5009

# Projeyi ve bağımlılıkları kopyalayın
COPY *.csproj .
RUN dotnet restore

# Kaynak kodu kopyalayın ve uygulamayı derleyin
COPY . .
RUN dotnet publish -c Release -o out

# 2. Aşama: Çalıştırma aşaması (Run Stage)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Build aşamasından derlenen çıktıyı kopyalayın
COPY --from=build /app/out .
ENV ASPNETCORE_URLS=http://*:5009

# Uygulamayı çalıştırın
ENTRYPOINT ["dotnet", "GermanVocabularyAPI.dll"]
