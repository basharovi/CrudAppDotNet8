# Use the SDK image for building and publishing
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory in the container
WORKDIR /app

# Copy the project file and restore dependencies
COPY ["CrudAppDotNet8.csproj", ""]
RUN dotnet restore "./CrudAppDotNet8.csproj"

# Copy the remaining source code
COPY . .

# Build the application
RUN dotnet build "CrudAppDotNet8.csproj" -c Release -o /app/build

# Publish the application
RUN dotnet publish "CrudAppDotNet8.csproj" -c Release -o /app/publish

# Use the runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Set the working directory in the container
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Expose ports
EXPOSE 8080
EXPOSE 8081

# Define the entry point for the container
ENTRYPOINT ["dotnet", "CrudAppDotNet8.dll"]
