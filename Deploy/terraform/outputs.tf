output "id" {
  value       = azurerm_key_vault.ecommerce_kv.id
  description = "The ID of the Key Vault."
}

output "name" {
  value       = azurerm_key_vault.ecommerce_kv.name
  description = "The name of the Key Vault."
}

output "uri" {
  value       = azurerm_key_vault.ecommerce_kv.vault_uri
  description = "The URI of the Key Vault."
}