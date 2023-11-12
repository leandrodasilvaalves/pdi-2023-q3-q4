resource "helm_release" "bacen" {
  name       = "bacen"
  chart      = "../../k8s/api-bank"
  values     = ["${file("../../k8s/bacen.values.yaml")}"]
  depends_on = [local_file.configure_kubectl, helm_release.kafka, helm_release.mongodb, helm_release.ingress-nginx]

  set {
    name  = "ingress.host"
    value = "bc.${var.dns}"
  }
}

resource "helm_release" "vulture" {
  name       = "vulture"
  chart      = "../../k8s/api-bank"
  values     = ["${file("../../k8s/vulture.values.yaml")}"]
  depends_on = [local_file.configure_kubectl, helm_release.bacen]

  set {
    name  = "ingress.host"
    value = "vulture.${var.dns}"
  }
}

resource "helm_release" "star-accounts" {
  name       = "star-accounts"
  chart      = "../../k8s/api-bank"
  values     = ["${file("../../k8s/star.accounts.values.yaml")}"]
  depends_on = [local_file.configure_kubectl, helm_release.bacen]

  set {
    name  = "ingress.host"
    value = "star.accounts.${var.dns}"
  }
}

resource "helm_release" "star-entries" {
  name       = "star-entries"
  chart      = "../../k8s/api-bank"
  values     = ["${file("../../k8s/star.entries.values.yaml")}"]
  depends_on = [local_file.configure_kubectl, helm_release.bacen]

  set {
    name  = "ingress.host"
    value = "star.entries.${var.dns}"
  }
}

resource "helm_release" "star-claims" {
  name       = "star-claims"
  chart      = "../../k8s/api-bank"
  values     = ["${file("../../k8s/star.claims.values.yaml")}"]
  depends_on = [local_file.configure_kubectl, helm_release.bacen]

  set {
    name  = "ingress.host"
    value = "star.claims.${var.dns}"
  }
}
