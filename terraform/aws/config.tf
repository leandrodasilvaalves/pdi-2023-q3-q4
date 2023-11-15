resource "random_string" "suffix" {
  length  = 8
  special = false
}

locals {
  cluster_name = "${var.proj_name}-${random_string.suffix.result}"
  arn_fullname = "arn:aws:eks:${var.region}:${var.account_id}:cluster/${local.cluster_name}"
  kubeconfig   = <<KUBECONFIG
apiVersion: v1
clusters:
- cluster:
    certificate-authority-data: ${module.eks.cluster_certificate_authority_data}
    server: ${module.eks.cluster_endpoint}
  name: ${local.arn_fullname}
contexts:
- context:
    cluster: ${local.arn_fullname}
    user: ${local.arn_fullname}
  name: ${local.arn_fullname}
current-context: ${local.arn_fullname}
kind: Config
preferences: {}
users:
- name: ${local.arn_fullname}
  user:
    exec:
      apiVersion: client.authentication.k8s.io/v1beta1
      args:
      - --region
      - ${var.region}
      - eks
      - get-token
      - --cluster-name
      - ${local.cluster_name}
      - --output
      - json
      - --profile
      - default
      command: aws
KUBECONFIG
}
