# Use the latest .NET SDK for building, packing, and publishing
FROM mcr.microsoft.com/dotnet/sdk:latest AS build

WORKDIR /app

# Copy only project files first to cache better (optional, but smart)
COPY . ./
RUN dotnet restore

# Restore as a separate step (better for caching)
RUN dotnet restore


# Clean previous builds
RUN dotnet clean

# Build the project
RUN dotnet build --configuration Release --no-restore

# Pack the NuGet package
RUN dotnet pack --configuration Release --no-build --output /nupkgs

# Set environment variable for API key (passed during docker build)
ARG NUGET_API_KEY
ENV NUGET_API_KEY=${NUGET_API_KEY}

# Push the NuGet package
ENTRYPOINT ["sh", "-c", "dotnet nuget push /nupkgs/*.nupkg --api-key $NUGET_API_KEY --source https://api.nuget.org/v3/index.json"]
