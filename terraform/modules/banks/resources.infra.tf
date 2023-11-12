resource "local_file" "configure_kubectl" {
  content  = var.raw_kubeconfig
  filename = pathexpand("~/.kube/do-config.yaml")
}

resource "helm_release" "ingress-nginx" {
  name             = "ingress"
  repository       = "https://kubernetes.github.io/ingress-nginx"
  chart            = "ingress-nginx"
  version          = "4.5.2"
  namespace        = "nginx-ingress"
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

  set {
    name  = "ingress.host"
    value = "kafka-ui.${var.dns}"
  }
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

  set {
    name  = "ingress.host"
    value = "mongo-express.${var.dns}"
  }
}
