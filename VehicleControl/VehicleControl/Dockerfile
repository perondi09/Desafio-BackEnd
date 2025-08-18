# =========================================================================================
# Estágio 1: Base - Define a imagem de runtime
# =========================================================================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Cria um usuário não-root para executar a aplicação por questões de segurança.
RUN adduser -u 1001 --disabled-password --gecos "" appuser
USER appuser

# =========================================================================================
# Estágio 2: Build - Compila o código da aplicação
# =========================================================================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia o .csproj e restaura as dependências para aproveitar o cache do Docker.
COPY VehicleControl.csproj ./
RUN dotnet restore "VehicleControl.csproj"

# Copia o restante dos arquivos e compila.
COPY . .
WORKDIR "/src/."
RUN dotnet build "VehicleControl.csproj" -c $BUILD_CONFIGURATION -o /app/build

# =========================================================================================
# Estágio 3: Publish - Publica a aplicação
# =========================================================================================
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "VehicleControl.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# =========================================================================================
# Estágio 4: Final - Monta a imagem final a partir da base
# =========================================================================================
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VehicleControl.dll"]
