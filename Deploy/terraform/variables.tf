variable "location" {
  type    = string
  default = "northeurope"
}
 
variable environment{
    description = "dev, test, staging, prod"
    type = string
}

variable storage_tier{
    description = "tier for the storage account"
    type = string
}

variable storage_replication_type{
    description = "replication type for the storage account"
    type = string
}
 