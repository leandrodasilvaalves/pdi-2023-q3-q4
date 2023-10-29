# AWS

Configurar `kubectl` para se conectar ao cluster
```sh
    aws eks update-kubeconfig --name {{cluster-name}}
```

## Create Node
- https://docs.aws.amazon.com/eks/latest/userguide/getting-started-console.html

```json
{
    "Role": {
        "Path": "/",
        "RoleName": "myAmazonEKSNodeRole",
        "RoleId": "AROAXTDYON6QFLD26ESUI",
        "Arn": "arn:aws:iam::{{accountnumber}}:role/myAmazonEKSNodeRole",
        "CreateDate": "2023-10-23T22:14:38+00:00",
        "AssumeRolePolicyDocument": {
            "Version": "2012-10-17",
            "Statement": [
                {
                    "Effect": "Allow",
                    "Principal": {
                        "Service": "ec2.amazonaws.com"
                    },
                    "Action": "sts:AssumeRole"
                }
            ]
        }
    }
}
```
## Get All Resources
```sh
aws resourcegroupstaggingapi get-resources --region us-east-2 >> resources.json
```

## Terraform
Configurar `kubectl` para se conectar ao cluster
```sh
aws eks --region $(terraform output -raw region) update-kubeconfig \
    --name $(terraform output -raw cluster_name)
```
