# ================================
# 1. Build Stage
# ================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy only project files first (for caching)
COPY ICTAWebsiteAPI/*.csproj ./ICTAWebsiteAPI/
COPY Business/*.csproj ./Business/
COPY Core/*.csproj ./Core/
COPY Data/*.csproj ./Data/
COPY DTOs/*.csproj ./DTOs/
COPY Entities/*.csproj ./Entities/

# Restore dependencies
RUN dotnet restore ICTAWebsiteAPI/ICTAWebsiteAPI.csproj

# Copy all project files
COPY . .

# Build and publish
RUN dotnet publish ICTAWebsiteAPI/ICTAWebsiteAPI.csproj -c Release -o /app/out

# ================================
# 2. Runtime Stage
# ================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 8080

ENTRYPOINT ["dotnet", "ICTAWebsiteAPI.dll"]
