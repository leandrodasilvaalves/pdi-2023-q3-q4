


module "eks" {
  source  = "terraform-aws-modules/eks/aws"
  version = "19.15.3"

  cluster_name    = local.cluster_name
  cluster_version = "1.27"

  vpc_id     = var.vpc_id
  subnet_ids = data.aws_subnets.default.ids

  cluster_endpoint_public_access = true

  eks_managed_node_group_defaults = {
    ami_type = var.ami_type
  }

  eks_managed_node_groups = {
    one = {
      name           = "node-group-1-${random_string.suffix.result}"
      instance_types = var.eks_node_groups_instance_types
      min_size       = var.eks_node_groups_min_size
      max_size       = var.eks_node_groups_max_size
      desired_size   = var.eks_node_groups_desiredsize
    }
  }
}

resource "local_file" "configure_kubectl" {
  content    = local.kubeconfig
  filename   = pathexpand("~/.kube/do-config.yaml")
  depends_on = [module.eks]
}