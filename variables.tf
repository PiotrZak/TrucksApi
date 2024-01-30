variable "app_settings" {
  type = map(object({
    LogLevel = map(string),
    ConnectionStrings = map(string),
    AllowedHosts = string
  }))
}

variable "resource_group_name" {
  description = "Name of the Azure Resource Group"
}

variable "location" {
  description = "Location for Azure resources"
}

variable "database_name" {
  description = "Name of the SQL Database"
}

variable "database_username" {
  description = "Username for the SQL Database"
}

variable "database_password" {
  description = "Password for the SQL Database"
}

variable "app_service_name" {
  description = "Name of the Azure App Service"
}

variable "api_container_image" {
  description = "Docker container image for the .NET API"
}
