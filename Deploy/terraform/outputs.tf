output "id" {
  value       = azurerm_key_vault.ecommerce_kv[0].id
  description = "The ID of the Key Vault."
}

output "name" {
  value       = azurerm_key_vault.ecommerce_kv[0].name
  description = "The name of the Key Vault."
}

output "uri" {
  value       = azurerm_key_vault.ecommerce_kv[0].vault_uri
  description = "The URI of the Key Vault."
}