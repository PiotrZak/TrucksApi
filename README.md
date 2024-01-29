# TrucksApi
API for recruitment task to company Coldrun.

REST API based on CQRS & Event Sourcing Pattern.


- Based on different operating system the different image for db working:
FROM mcr.microsoft.com/azure-sql-edge:latest
FROM mcr.microsoft.com/mssql/server:2019-latest

cle
dotnet publish -c Release
ls bin/Release/net7.0/publish

docker run -p 80:5295 -d your-image-name

ipconfig getifaddr en0

When running in docker-compose, then connection string is:

    "DBConnection": "Server=db;Database=trucks-db;User Id=sa;Password=password123!;TrustServerCertificate=True;"

- docker-compose build
- docker-compose up

kubectl apply -f deployment.yml
kubectl apply -f service.yml


docker build -t trucks-api .
(cause kube required image with this tag)


Todo:

- Test with Kubernetes
- Automatically initiate create-database.sql (not via Azure Data Studio)