resource "digitalocean_kubernetes_cluster" "pdi-2023-q3-q4" {
  name    = var.k8s_name
  region  = var.k8s_region
  version = var.k8s_version
  tags    = var.k8s_tags

  node_pool {
    name       = "${var.k8s_name}-${var.k8s_node_pool_name}"
    size       = var.k8s_node_size
    node_count = var.k8s_node_count
  }
}

resource "local_file" "configure_kubectl" {
  content  = digitalocean_kubernetes_cluster.pdi-2023-q3-q4.kube_config[0].raw_config
  filename = pathexpand("~/.kube/do-config.yaml")

}

module "resources_md" {
  source = "../modules"
  depends_on = [
    local_file.configure_kubectl
  ]
}
