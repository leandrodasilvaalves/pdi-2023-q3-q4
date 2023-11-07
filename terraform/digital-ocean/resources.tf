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

resource "helm_release" "ingress-nginx" {
  name             = "nginx-ingress"
  repository       = "https://kubernetes.github.io/ingress-nginx"
  chart            = "ingress-nginx"
  version          = "4.5.2"
  namespace        = "ingress-nginx"
  create_namespace = true
  depends_on       = [digitalocean_kubernetes_cluster.pdi-2023-q3-q4]
}

resource "helm_release" "kafka" {
  name       = "kafka"
  repository = "https://charts.bitnami.com/bitnami"
  chart      = "kafka"
  version    = "26.3.1"
  values     = ["${file("../../k8s/kafka/values.yaml")}"]
  depends_on = [digitalocean_kubernetes_cluster.pdi-2023-q3-q4]
}

resource "helm_release" "kafka-ui" {
  name       = "kafka-ui"
  repository = "https://provectus.github.io/kafka-ui-charts"
  chart      = "kafka-ui"
  version    = "0.7.5"
  values     = ["${file("../../k8s/kafka/ui/values.yaml")}"]
  depends_on = [digitalocean_kubernetes_cluster.pdi-2023-q3-q4, helm_release.kafka]
}

resource "helm_release" "mongodb" {
  name       = "mongo"
  repository = "https://charts.bitnami.com/bitnami"
  chart      = "mongodb"
  version    = "14.2.0"
  values     = ["${file("../../k8s/mongo/values.yaml")}"]
  depends_on = [digitalocean_kubernetes_cluster.pdi-2023-q3-q4]
}

resource "helm_release" "mongo-express" {
  name       = "mongo-express"
  chart      = "../../k8s/mongo/ui"
  depends_on = [digitalocean_kubernetes_cluster.pdi-2023-q3-q4, helm_release.mongodb]
}

