# TrucksApi
API for recruitment task to company Coldrun.

REST API based on CQRS & Event Sourcing Pattern.

- Based on different operating system the different image for db working:
FROM mcr.microsoft.com/azure-sql-edge:latest
FROM mcr.microsoft.com/mssql/server:2019-latest

(If using windows, change the image).

The simplest option to run aplication is via docker-compose:
When running in docker-compose, then connection string is:

    "DBConnection": "Server=db;Database=trucks-db;User Id=sa;Password=password123!;TrustServerCertificate=True;"

- docker-compose build
- docker-compose up

After running project - needs to execute create-database.sql script in db instance (eg. via Azure Data Studio).

____

dotnet publish -c Release
ls bin/Release/net7.0/publish

docker run -p 80:5295 -d your-image-name

ipconfig getifaddr en0

____

https://www.knowledgehut.com/blog/devops/kubernetes-dashboard

kubectl apply -f deployment.yaml
kubectl apply -f service.yaml

kubectl get pods
kubectl get deployments
kubectl get services


kubectl port-forward service/trucks-api-service 8070:8070

Forwarding from 127.0.0.1:8070 -> 80
Forwarding from [::1]:8070 -> 80


kubectl rollout restart deployment my-deployment
kubectl proxy



docker build -t trucks-api .
(cause kube required image with this tag)

Added Terraform for experimentation purpose.

- terraform init
- terraform plan
