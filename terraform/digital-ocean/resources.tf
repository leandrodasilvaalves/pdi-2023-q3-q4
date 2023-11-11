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

resource "helm_release" "ingress-nginx" {
  name             = "nginx-ingress"
  repository       = "https://kubernetes.github.io/ingress-nginx"
  chart            = "ingress-nginx"
  version          = "4.5.2"
  namespace        = "ingress-nginx"
  create_namespace = true
  depends_on       = [local_file.configure_kubectl]
}

resource "helm_release" "kafka" {
  name       = "kafka"
  repository = "https://charts.bitnami.com/bitnami"
  chart      = "kafka"
  version    = "26.3.1"
  values     = ["${file("../../k8s/kafka/values.yaml")}"]
  depends_on = [local_file.configure_kubectl]
}

resource "helm_release" "kafka-ui" {
  name       = "kafka-ui"
  repository = "https://provectus.github.io/kafka-ui-charts"
  chart      = "kafka-ui"
  version    = "0.7.5"
  values     = ["${file("../../k8s/kafka/ui/values.yaml")}"]
  depends_on = [local_file.configure_kubectl, helm_release.kafka, helm_release.ingress-nginx]
}

resource "helm_release" "mongodb" {
  name       = "mongo"
  repository = "https://charts.bitnami.com/bitnami"
  chart      = "mongodb"
  version    = "14.2.0"
  values     = ["${file("../../k8s/mongo/values.yaml")}"]
  depends_on = [local_file.configure_kubectl]
}

resource "helm_release" "mongo-express" {
  name       = "mongo-express"
  chart      = "../../k8s/mongo/ui"
  depends_on = [local_file.configure_kubectl, helm_release.mongodb, helm_release.ingress-nginx]
}

resource "helm_release" "bacen" {
  name       = "bacen"
  chart      = "../../k8s/api-bank"
  values     = ["${file("../../k8s/bacen.values.yaml")}"]
  depends_on = [helm_release.kafka, helm_release.mongodb, helm_release.ingress-nginx]
}

resource "helm_release" "vulture" {
  name       = "vulture"
  chart      = "../../k8s/api-bank"
  values     = ["${file("../../k8s/vulture.values.yaml")}"]
  depends_on = [helm_release.bacen]
}

resource "helm_release" "star-accounts" {
  name       = "star-accounts"
  chart      = "../../k8s/api-bank"
  values     = ["${file("../../k8s/star.accounts.values.yaml")}"]
  depends_on = [helm_release.bacen]
}

resource "helm_release" "star-entries" {
  name       = "star-entries"
  chart      = "../../k8s/api-bank"
  values     = ["${file("../../k8s/star.entries.values.yaml")}"]
  depends_on = [helm_release.bacen]
}

resource "helm_release" "star-claims" {
  name       = "star-claims"
  chart      = "../../k8s/api-bank"
  values     = ["${file("../../k8s/star.claims.values.yaml")}"]
  depends_on = [helm_release.bacen]
}