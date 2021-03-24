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

  app_settings ={
    https_only = true
  }
  site_config {
    cors {
      allowed_origins = ["*"]
    }
  }
}

data "azurerm_subscription" "current" {
}

data "shell_script" "functions_key" {
  lifecycle_commands {
    read = file("${path.module}/readkey.sh")
  }
  environment = {
    FUNC_NAME = azurerm_function_app.ecommerce_function_app.name
    RG_NAME   = azurerm_resource_group.ecommerce_rg.name
    SUB_ID    = data.azurerm_subscription.current.subscription_id
  }
  depends_on = [azurerm_function_app.ecommerce_function_app]

}


data "azuredevops_project" "project" {
  name = "ecommerce"
}

resource "azuredevops_variable_group" "variablegroup" {
  project_id   = data.azuredevops_project.project.id
  name         = "${var.environment_short}-ecommerce-outputs"
  description  = "${var.environment_short}-ecommerce-outputs"
  allow_access = true
  variable {
    name  = "function_master_key"
    value = try(data.shell_script.functions_key.output["masterKey"], "")
  }

  variable {
    name  = "function_hostname"
    value = azurerm_function_app.functions.default_hostname
  }

  variable {
    name  = "function_name"
    value = azurerm_function_app.functions.name
  }
}
