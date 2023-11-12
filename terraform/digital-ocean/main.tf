terraform {
  required_providers {
    digitalocean = {
      source  = "digitalocean/digitalocean"
      version = "~> 2.0"
    }
  }
}

provider "digitalocean" {
  token = var.do_token
}

provider "helm" {
  kubernetes {
    config_path = pathexpand("~/.kube/do-config.yaml")
  }
}