#!/bin/bash

CLUSTER_NAME=pdi-2023-q3-q4

if [[ $1 == *"-x"* ]]; then
  kind delete cluster --name $CLUSTER_NAME
else
  kind create cluster --name $CLUSTER_NAME --config=- <<EOF
kind: Cluster
apiVersion: kind.x-k8s.io/v1alpha4
nodes:
- role: control-plane
  kubeadmConfigPatches:
  - |
    kind: InitConfiguration
    nodeRegistration:
      kubeletExtraArgs:
        node-labels: "ingress-ready=true"
  extraPortMappings:
  - containerPort: 80
    hostPort: 80
    protocol: TCP
EOF

  echo installing ingress ...
  kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/main/deploy/static/provider/kind/deploy.yaml

  #https://github.com/kubernetes-sigs/metrics-server
  echo installing metrics server ...
  kubectl apply -f https://github.com/kubernetes-sigs/metrics-server/releases/latest/download/components.yaml


  echo waiting ...
  kubectl wait --namespace ingress-nginx \
    --for=condition=ready pod \
    --selector=app.kubernetes.io/component=controller \
    --timeout=120s
fi