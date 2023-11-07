variable "do_token" {}

variable "k8s_name" {
  type        = string
  default     = "pdi-2023-q3-q4"
  description = "Informe a nome do cluster"
}

variable "k8s_region" {
  type        = string
  default     = "nyc1"
  description = "Informe a região do cluster"
}

variable "k8s_version" {
  type        = string
  default     = "1.28.2-do.0"
  description = "Informe a versão do cluster"
}

variable "k8s_tags" {
  type        = list(string)
  default     = ["devops", "pdi", "q3-q4", "terraform"]
  description = "Informe as tags do cluster"
}

variable "k8s_node_pool_name" {
  type        = string
  default     = "default"
  description = "Informe o nome do node pool"
}

variable "k8s_node_count" {
  type        = number
  default     = 2
  description = "Informe a quantidade de nós o node poll deverá ter"
}

variable "k8s_node_size" {
  type        = string
  default     = "s-2vcpu-2gb"
  description = "Informe o tamanho de cada nó do node pool"
}
