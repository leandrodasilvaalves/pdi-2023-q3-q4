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
- role: worker
- role: worker
- role: worker
- role: worker
EOF

kubectl apply -f https://raw.githubusercontent.com/metallb/metallb/v0.13.7/config/manifests/metallb-native.yaml

kubectl wait --namespace metallb-system \
                --for=condition=ready pod \
                --selector=app=metallb \
                --timeout=120s

# Get the IP address range
DOCKER_OUTPUT=$(docker network inspect -f '{{.IPAM.Config}}' kind)
RANGE_IP_ADDRESS=$(echo "$DOCKER_OUTPUT" | grep -o -m 1 '[0-9]\+\.[0-9]\+' | head -1) 
echo "Range IP: $RANGE_IP_ADDRESS"

# apply loadbalancer configuration
kubectl apply -f - <<EOF
apiVersion: metallb.io/v1beta1
kind: IPAddressPool
metadata:
    name: example
    namespace: metallb-system
spec:
    addresses:
    - $RANGE_IP_ADDRESS.255.200-$RANGE_IP_ADDRESS.255.250
---
apiVersion: metallb.io/v1beta1
kind: L2Advertisement
metadata:
    name: empty
    namespace: metallb-system
EOF
fi
# Ref: https://kind.sigs.k8s.io/docs/user/loadbalancer/