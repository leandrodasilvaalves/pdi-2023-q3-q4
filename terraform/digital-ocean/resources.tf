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
  destroy_all_associated_resources = true
}

resource "null_resource" "kubeconfig" {
  provisioner "local-exec" {
    command = "doctl kubernetes cluster kubeconfig save ${digitalocean_kubernetes_cluster.pdi-2023-q3-q4.id} --set-current-context"
  }
  depends_on = [digitalocean_kubernetes_cluster.pdi-2023-q3-q4]
}
