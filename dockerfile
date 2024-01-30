FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App

# Copy everything
COPY ./Trucks.API ./
COPY ./Trucks.API.Tests ./

# Restore as distinct layers
RUN dotnet restore Trucks.API.csproj
# Build and publish a release
RUN dotnet publish "Trucks.API.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
COPY --from=build-env /App/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "Trucks.API.dll"]