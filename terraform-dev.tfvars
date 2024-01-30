resource_group_name = "<your-resource-group>"
location            = "<your-location>"
database_name       = "<your-database-name>"
database_username   = "<your-database-username>"
database_password   = "<your-database-password>"
app_service_name    = "<your-app-service-name>"
api_container_image = "<your-api-container-image>"

app_settings = {
  LogLevel = {
    Default = "Information",
    "Microsoft.AspNetCore" = "Warning"
  }
  ConnectionStrings = {
    DBConnection = "Server=localhost;Database=trucks-db;User Id=sa;Password=password123!;TrustServerCertificate=True;"
  }
  AllowedHosts = "*"
}