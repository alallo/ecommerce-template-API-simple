output "id" {
  value       = var.enabled == true ? azurerm_key_vault.keyvault[0].id : null
  description = "The ID of the Key Vault."
}

output "name" {
  value       = var.enabled == true ? azurerm_key_vault.keyvault[0].name : null
  description = "The name of the Key Vault."
}

output "uri" {
  value       = var.enabled == true ? azurerm_key_vault.keyvault[0].vault_uri : null
  description = "The URI of the Key Vault."
}