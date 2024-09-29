# Use the .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["AIM.csproj", "./"] 
RUN dotnet restore

# Copy all other files and build the application
COPY . . 
WORKDIR "/src"
RUN dotnet build "AIM.csproj" -c Release -o /app/build

# Use the .NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "AIM.dll"]
