provider "azurerm" {
    features {}
}
provider "azuredevops" {
  org_service_url = "https://dev.azure.com/alessandrolallo0821"
  personal_access_token = var.personal_token
}

