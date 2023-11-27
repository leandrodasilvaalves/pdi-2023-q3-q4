#!/bin/bash

SERVICE_TYPE=ClusterIP
if [[ $2 == *"--lb"* ]]; then
  SERVICE_TYPE='LoadBalancer'
fi

if [[ $1 == *"-i"* ]]; then
  if [[ $2 == *"--cloud"* ]]; then
    helm install ingress ingress-nginx/ingress-nginx
  fi
  helm install kafka k8s/kafka/confluent -f k8s/kafka/values.yaml
  helm install kafka-ui kafka-ui/kafka-ui -f k8s/kafka/ui/values.yaml --set service.type=$SERVICE_TYPE
  helm install mongo k8s/mongo -f k8s/mongo/values.yaml
  helm install bacen k8s/api-bank -f k8s/bacen.values.yaml --set service.type=$SERVICE_TYPE
  helm install vulture k8s/api-bank -f k8s/vulture.values.yaml --set service.type=$SERVICE_TYPE
  helm install star-accounts k8s/api-bank -f k8s/star.accounts.values.yaml --set service.type=$SERVICE_TYPE
  helm install star-entries k8s/api-bank -f k8s/star.entries.values.yaml --set service.type=$SERVICE_TYPE
  helm install star-claims k8s/api-bank -f k8s/star.claims.values.yaml --set service.type=$SERVICE_TYPE
fi

if [[ $1 == *"-x"* ]]; then
  helm uninstall kafka
  helm uninstall kafka-ui
  helm uninstall mongo
  helm uninstall bacen
  helm uninstall vulture
  helm uninstall star-accounts
  helm uninstall star-entries
  helm uninstall star-claims
fi

# watch kubectl get pods
# watch -n 10 'kubectl get services --no-headers | awk "{print \$1, \$4}"'
