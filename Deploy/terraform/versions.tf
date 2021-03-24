terraform {
  required_providers {
    azuredevops = {
      source = "microsoft/azuredevops"
    }
    azurerm = {
      source = "hashicorp/azurerm"
    }
    shell = {
      source  = "scottwinkler/shell"
      version = "1.7.7"
    }
  }
  required_version = ">= 0.13"
}