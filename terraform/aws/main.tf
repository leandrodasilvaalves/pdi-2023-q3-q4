terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 5.7.0"
    }

    random = {
      source  = "hashicorp/random"
      version = "~> 3.5.1"
    }
    helm = {
      source  = "hashicorp/helm"
      version = "2.11.0"
    }
  }
  required_version = "~> 1.3"
}

provider "aws" {
  region = var.region
}

provider "helm" {
  kubernetes {
    config_path = pathexpand("~/.kube/do-config.yaml")
  }
}
