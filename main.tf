provider "azurerm" {
  features = {}
}


resource "azurerm_resource_group" "example" {
  name     = var.resource_group_name
  location = var.location
}

resource "azurerm_sql_server" "example" {
  name                         = "sql-server"
  resource_group_name          = azurerm_resource_group.example.name
  location                     = azurerm_resource_group.example.location
  version                      = "12.0"
  administrator_login          = var.database_username
  administrator_login_password = var.database_password
}

resource "azurerm_sql_database" "example" {
  name                = var.database_name
  resource_group_name = azurerm_resource_group.example.name
  location            = azurerm_resource_group.example.location
  server_name         = azurerm_sql_server.example.name
  edition             = "Standard"
  collation           = "SQL_Latin1_General_CP1_CI_AS"
  max_size_gb         = 1
}


resource "azurerm_app_service_plan" "example" {
  name                = "app-service-plan"
  resource_group_name = azurerm_resource_group.example.name
  location            = azurerm_resource_group.example.location
  sku {
    tier = "Basic"
    size = "B1"
  }
}

resource "azurerm_app_service" "example" {
  name                = var.app_service_name
  resource_group_name = azurerm_resource_group.example.name
  location            = azurerm_resource_group.example.location
  app_service_plan_id = azurerm_app_service_plan.example.id

  site_config {
    dotnet_framework_version = "v5.0"
  }

  app_settings = {
    "ASPNETCORE_ENVIRONMENT" = "Production"
  }
}

resource "azurerm_container_registry" "example" {
  name                = "containerregistry"
  resource_group_name = azurerm_resource_group.example.name
  location            = azurerm_resource_group.example.location
  sku                 = "Standard"
}

resource "azurerm_container_image" "example" {
  name                = "api-image"
  resource_group_name = azurerm_resource_group.example.name
  registry_login_server = azurerm_container_registry.example.login_server
  username = azurerm_container_registry.example.admin_username
  password = azurerm_container_registry.example.admin_password
  image = var.api_container_image
}

resource "azurerm_web_app" "example" {
  name                = "web-app"
  resource_group_name = azurerm_resource_group.example.name
  location            = azurerm_resource_group.example.location
  app_service_plan_id = azurerm_app_service_plan.example.id

  site_config {
    always_on = true
  }

  app_settings = {
    "WEBSITES_PORT" = "80",
    "LogLevel_Default" = var.app_settings["Logging"]["LogLevel"]["Default"],
    "LogLevel_Microsoft_AspNetCore" = var.app_settings["Logging"]["LogLevel"]["Microsoft.AspNetCore"],
    "DBConnection" = var.app_settings["ConnectionStrings"]["DBConnection"],
    "AllowedHosts" = var.app_settings["AllowedHosts"]
  }

  connection_string {
    name  = "DatabaseConnectionString"
    type  = "SQLServer"
    value = var.app_settings["ConnectionStrings"]["DBConnection"]
  }
}
