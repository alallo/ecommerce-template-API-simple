resource "azurerm_resource_group" "ecommerce_rg" {
  name     = "rg-ecomm-${var.environment}-${var.location_short}-01"
  location = var.location
}

resource "azurerm_storage_account" "ecommerce_storage" {
  name                     = "saecomm${var.environment}${var.location_short}01"
  resource_group_name      = azurerm_resource_group.ecommerce_rg.name
  location                 = azurerm_resource_group.ecommerce_rg.location
  account_tier             = var.storage_tier
  account_replication_type = var.storage_replication_type
}

data "azurerm_client_config" "current" {}

resource "azurerm_key_vault" "ecommerce_kv" {
  name                        = "kvecomm${var.environment}${var.location_short}01"
  location                    = azurerm_resource_group.ecommerce_rg.location
  resource_group_name         = azurerm_resource_group.ecommerce_rg.name
  enabled_for_disk_encryption = true
  tenant_id                   = data.azurerm_client_config.current.tenant_id
  soft_delete_retention_days  = 7
  purge_protection_enabled    = false

  sku_name = "standard"

  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = data.azurerm_client_config.current.object_id

   key_permissions = [
      "create",
      "get",
    ]

    secret_permissions = [
      "set",
      "get",
      "delete",
      "purge",
      "recover"
    ]
  }
}

resource "azurerm_key_vault_secret" "ecommerce_secret" {
  name         = var.sendgrid_secret_name
  value        = var.sendgrid_secret_value
  key_vault_id = azurerm_key_vault.ecommerce_kv.id
}

resource "azurerm_app_service_plan" "ecommerce_service_plan" {
  name                = "asp-ecomm-${var.environment}-${var.location_short}-01"
  location            = azurerm_resource_group.ecommerce_rg.location
  resource_group_name = azurerm_resource_group.ecommerce_rg.name
  kind                = "FunctionApp"

  sku {
    tier = "Dynamic"
    size = "Y1"
  }
}

resource "azurerm_function_app" "ecommerce_function_app" {
  name                       = "fa-ecomm-${var.environment}-${var.location_short}-01"
  location                   = azurerm_resource_group.ecommerce_rg.location
  resource_group_name        = azurerm_resource_group.ecommerce_rg.name
  app_service_plan_id        = azurerm_app_service_plan.ecommerce_service_plan.id
  storage_account_name       = azurerm_storage_account.ecommerce_storage.name
  storage_account_access_key = azurerm_storage_account.ecommerce_storage.primary_access_key
  version = "~3"
  app_settings ={
    https_only = true
  }
  site_config {
    cors {
      allowed_origins = ["*"]
    }
  }
}

data "azurerm_function_app_host_keys" "function_keys" {
  name                = azurerm_function_app.ecommerce_function_app.name
  resource_group_name = azurerm_resource_group.ecommerce_rg.name
  depends_on = [azurerm_function_app.ecommerce_function_app]
}

data "azuredevops_project" "project" {
  name = "ecommerce"
}

resource "azuredevops_variable_group" "variablegroup" {
  project_id   = data.azuredevops_project.project.id
  name         = "${var.environment}-ecommerce-outputs"
  description  = "${var.environment}-ecommerce-outputs"
  allow_access = true
  variable {
    name  = "function_master_key"
    value = data.azurerm_function_app_host_keys.function_keys.default_function_key
  }

  variable {
    name  = "function_hostname"
    value = azurerm_function_app.ecommerce_function_app.default_hostname
  }

  variable {
    name  = "function_name"
    value = azurerm_function_app.ecommerce_function_app.name
  }
}
