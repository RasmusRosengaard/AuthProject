# Base image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["./", "./"]
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV DB_PATH=/data/auth.db
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "webapi.dll"]