output "cluster_id" {
  value = digitalocean_kubernetes_cluster.pdi-2023-q3-q4.id
}

output "cluster_endpoint" {
  value = digitalocean_kubernetes_cluster.pdi-2023-q3-q4.endpoint
}

output "cluster_region" {
  value = digitalocean_kubernetes_cluster.pdi-2023-q3-q4.region
}

output "cluster_tags" {
  value = digitalocean_kubernetes_cluster.pdi-2023-q3-q4.tags
}

output "kubeconfig" {
  value     = digitalocean_kubernetes_cluster.pdi-2023-q3-q4.kube_config[0]
  sensitive = true
}