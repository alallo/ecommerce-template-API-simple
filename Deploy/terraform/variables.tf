variable "location" {
  type    = string
  default = "northeurope"
}

variable "location_short" {
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

variable personal_token{
    description = "personal token used to create a new variable group"
    type = string
}

variable sendgrid_secret_name{
  description = "sendgrid secret name"
  type = string
}

variable sendgrid_secret_value {
  description = "sendgrid secret value"
  type = string
}
 