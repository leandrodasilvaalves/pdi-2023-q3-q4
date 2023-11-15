variable "proj_name" {
  description = "Project Name"
  type        = string
  default     = "pdi-2023-q3-q4"
}

variable "region" {
  description = "AWS region"
  type        = string
  default     = "us-east-1"
}

variable "account_id" {
  description = "AWS account Id"
  type        = string
  default     = "522091130784"
}

variable "eks_node_groups_instance_types" {
  type = list(string)
  default = [ "t3.medium" ]
  description = "Tipos de instância do nó"
}

variable "eks_node_groups_min_size" {
  type = number
  default = 1
  description = "Número mínimo de instâncias no nó"
}

variable "eks_node_groups_max_size" {
  type = number
  default = 3
  description = "Número máximo de instâncias no nó"
}

variable "eks_node_groups_desiredsize" {
  type = number
  default = 2
  description = "Número desejadode instâncias no nó"
}

variable "dns" {
  type = string
  default = "leandroalves.dev.br"
  description = "DNS utilizando no ingress controller dos serviços"
}

variable "ami_type" {
    type = string
  default = "AL2_x86_64"
  description = "Tipo de imagem AWS associada com o nó"
}