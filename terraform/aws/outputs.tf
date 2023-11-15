output "cluster_endpoint" {
  description = "Endpoint for EKS control plane"
  value       = module.eks.cluster_endpoint
}

output "cluster_security_group_id" {
  description = "Security group ids attached to the cluster control plane"
  value       = module.eks.cluster_security_group_id
}

output "region" {
  description = "AWS region"
  value       = var.region
}

output "cluster_name" {
  description = "Kubernetes cluster name"
  value       = module.eks.cluster_name
}

output "kubeconfig-certificate-authority-data" {
  value     = module.eks.cluster_certificate_authority_data
  sensitive = true
}

output "kubeconfig" {
  value     = local.kubeconfig
  sensitive = true
}
