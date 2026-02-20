# ---------- Build stage ----------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Kopiér csproj først (bedre cache)
COPY *.csproj .
RUN dotnet restore

# Kopiér resten
COPY . .
RUN dotnet publish -c Release -o /app/publish

# ---------- Runtime stage ----------
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

COPY --from=build /app/publish .

# Mappe til SQLite database (volume mount)
RUN mkdir /data

EXPOSE 8080

ENTRYPOINT ["dotnet", "webapi.dll"]