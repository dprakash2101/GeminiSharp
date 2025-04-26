# Use the latest .NET SDK for building and publishing
FROM mcr.microsoft.com/dotnet/sdk:latest

WORKDIR /app

# Copy all files (including .csproj) to the working directory
COPY . ./

# Restore dependencies (specify the .csproj file explicitly if needed)
# Replace 'YourProject.csproj' with your actual project file name
RUN dotnet restore "YourProject.csproj"

# Clean the solution to remove any stale artifacts
RUN dotnet clean "YourProject.csproj" --configuration Release

# Build and pack the NuGet package
RUN dotnet build "YourProject.csproj" --configuration Release --no-restore
RUN dotnet pack "YourProject.csproj" --configuration Release --no-build --output /nupkgs

# Set environment variable for API key
ARG NUGET_API_KEY
ENV NUGET_API_KEY=${NUGET_API_KEY}

# Publish the package to NuGet
ENTRYPOINT ["sh", "-c", "dotnet nuget push /nupkgs/*.nupkg --api-key $NUGET_API_KEY --source https://api.nuget.org/v3/index.json"]
