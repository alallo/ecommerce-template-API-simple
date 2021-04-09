output "kvid" {
  value       = azurerm_key_vault.ecommerce_kv.id
  description = "The ID of the Key Vault."
}

output "kvname" {
  value       = azurerm_key_vault.ecommerce_kv.name
  description = "The name of the Key Vault."
}

output "kvuri" {
  value       = azurerm_key_vault.ecommerce_kv.vault_uri
  description = "The URI of the Key Vault."
}