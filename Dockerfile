# Use latest .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:latest AS build

WORKDIR /app

# Copy solution and project files first (for caching)
COPY *.sln ./
COPY src/GeminiSharp/*.csproj src/GeminiSharp/

# Copy README file
COPY ../README.md /app/README.md

# Restore dependencies
WORKDIR /app/src/GeminiSharp
RUN dotnet restore

# Copy the rest of the code
WORKDIR /app
COPY . ./

# Clean old builds
WORKDIR /app/src/GeminiSharp
RUN dotnet clean

# Build and pack
RUN dotnet build --configuration Release --no-restore
RUN dotnet pack --configuration Release --no-build --output /nupkgs

# Publish
FROM mcr.microsoft.com/dotnet/sdk:latest AS publish

WORKDIR /app

COPY --from=build /nupkgs /nupkgs

ARG NUGET_API_KEY
ENV NUGET_API_KEY=${NUGET_API_KEY}

ENTRYPOINT ["sh", "-c", "dotnet nuget push /nupkgs/*.nupkg --api-key $NUGET_API_KEY --source https://api.nuget.org/v3/index.json"]
