# Use the latest .NET SDK as base image
FROM mcr.microsoft.com/dotnet/sdk:latest AS build

WORKDIR /app

# Copy project files and restore dependencies
COPY . ./
RUN dotnet restore

# Build and pack the NuGet package
RUN dotnet build --configuration Release --no-restore
RUN dotnet pack --configuration Release --no-build --output /nupkgs

# Final image for publishing
FROM mcr.microsoft.com/dotnet/runtime:latest AS runtime

WORKDIR /app

# Copy the built NuGet package
COPY --from=build /nupkgs /nupkgs

# Set the entrypoint to publish the package
ENTRYPOINT ["dotnet", "nuget", "push", "/nupkgs/*.nupkg", "--api-key", "${NUGET_API_KEY}", "--source", "https://api.nuget.org/v3/index.json"]
