# Use the latest .NET SDK for building and publishing
FROM mcr.microsoft.com/dotnet/sdk:latest AS build

WORKDIR /app

# Copy project files and restore dependencies
COPY . ./
RUN dotnet restore

# Build and pack the NuGet package
RUN dotnet build --configuration Release --no-restore
RUN dotnet pack --configuration Release --no-build --output /nupkgs

# Use SDK in final stage to ensure NuGet commands work
FROM mcr.microsoft.com/dotnet/sdk:latest AS publish

WORKDIR /app

# Copy the built NuGet package
COPY --from=build /nupkgs /nupkgs

# Set environment variable for API key
ARG NUGET_API_KEY
ENV NUGET_API_KEY=${NUGET_API_KEY}

# Publish the package to NuGet
ENTRYPOINT ["sh", "-c", "dotnet nuget push /nupkgs/*.nupkg --api-key $NUGET_API_KEY --source https://api.nuget.org/v3/index.json"]
