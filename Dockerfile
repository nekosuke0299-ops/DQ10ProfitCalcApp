# 1. ビルド環境の指定
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# プロジェクトファイルをコピーして復元
COPY ["ProfitCalcApp.csproj", "."]
RUN dotnet restore "./ProfitCalcApp.csproj"

# すべてのファイルをコピーしてビルド
COPY . .
RUN dotnet build "ProfitCalcApp.csproj" -c Release -o /app/build

# 2. 公開用環境の作成
FROM build AS publish
RUN dotnet publish "ProfitCalcApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 3. 実行用環境の作成
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# ポートの設定
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "ProfitCalcApp.dll"]