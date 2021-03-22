resource "azurerm_resource_group" "ecommerce_rg" {
  name     = "${var.environment}_azure_functions_ecommerce_rg"
  location = var.location
}

resource "azurerm_storage_account" "ecommerce_storage" {
  name                     = "${var.environment}ecommercesa"
  resource_group_name      = azurerm_resource_group.ecommerce_rg.name
  location                 = azurerm_resource_group.ecommerce_rg.location
  account_tier             = var.storage_tier
  account_replication_type = var.storage_replication_type
}

resource "azurerm_app_service_plan" "ecommerce_service_plan" {
  name                = "${var.environment}ecommercesp"
  location            = azurerm_resource_group.ecommerce_rg.location
  resource_group_name = azurerm_resource_group.ecommerce_rg.name
  kind                = "FunctionApp"

  sku {
    tier = "Dynamic"
    size = "Y1"
  }
}

resource "azurerm_function_app" "ecommerce_function_app" {
  name                       = "${var.environment}""${var.function_app_name}"
  location                   = azurerm_resource_group.ecommerce_rg.location
  resource_group_name        = azurerm_resource_group.ecommerce_rg.name
  app_service_plan_id        = azurerm_app_service_plan.ecommerce_service_plan.id
  storage_account_name       = azurerm_storage_account.ecommerce_storage.name
  storage_account_access_key = azurerm_storage_account.ecommerce_storage.primary_access_key
}