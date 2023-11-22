#!/bin/bash

SERVICE_TYPE=ClusterIP
if [[ $2 == *"--lb"* ]]; then
    SERVICE_TYPE='LoadBalancer'
fi

if [[ $1 == *"-i"* ]]; then
    # helm install ingress ingress-nginx/ingress-nginx
    helm install kafka k8s/kafka/confluent -f k8s/kafka/values.yaml
    helm install kafka-ui kafka-ui/kafka-ui -f k8s/kafka/ui/values.yaml --set service.type=$SERVICE_TYPE
    helm install mongo bitnami/mongodb -f k8s/mongo/values.yaml
    helm install mongo-express k8s/mongo/ui/   --set service.type=$SERVICE_TYPE
    helm install bacen k8s/api-bank -f k8s/bacen.values.yaml --set service.type=$SERVICE_TYPE
    helm install vulture k8s/api-bank -f k8s/vulture.values.yaml --set service.type=$SERVICE_TYPE
    helm install star-accounts k8s/api-bank -f k8s/star.accounts.values.yaml --set service.type=$SERVICE_TYPE
    helm install star-entries k8s/api-bank -f k8s/star.entries.values.yaml --set service.type=$SERVICE_TYPE
    helm install star-claims k8s/api-bank -f k8s/star.claims.values.yaml --set service.type=$SERVICE_TYPE
fi

if [[ $1 == *"-x"* ]]; then
    # helm uninstall ingress
    helm uninstall kafka
    helm uninstall kafka-ui
    helm uninstall mongo 
    helm uninstall mongo-express
    helm uninstall bacen
    helm uninstall vulture
    helm uninstall star-accounts
    helm uninstall star-entries
    helm uninstall star-claims
fi

if [[ $1 == *"-u"* ]]; then
    if [[ $2 == *"ingress"* ]]; then
        helm upgrade ingress ingress-nginx/ingress-nginx
    elif [[ $2 == *"kafka"* ]]; then
        helm upgrade kafka -f ./k8s/kafka/values.yaml
    elif [[ $2 == *"kafka-ui"* ]]; then
        helm upgrade kafka-ui -f ./k8s/kafka/ui/values.yaml --set service.type=$SERVICE_TYPE
    elif [[ $2 == *"mongo"* ]]; then
        helm upgrade mongo -f ./k8s/mongo/values.yaml
    elif [[ $2 == *"mongo-express"* ]]; then
        helm upgrade mongo-express ./k8s/mongo/ui --set service.type=$SERVICE_TYPE
    elif [[ $2 == *"bacen"* ]]; then 
        helm upgrade bacen ./k8s/api-bank -f k8s/bacen.values.yaml --set service.type=$SERVICE_TYPE
    elif [[ $2 == *"vulture"* ]]; then
        helm upgrade vulture k8s/api-bank -f k8s/vulture.values.yaml --set service.type=$SERVICE_TYPE
    elif [[ $2 == *"star-accounts"* ]]; then
        helm upgrade star-accounts k8s/api-bank -f k8s/star.accounts.values.yaml --set service.type=$SERVICE_TYPE
    elif [[ $2 == *"star-entries"* ]]; then
        helm upgrade star-entries k8s/api-bank -f k8s/star.entries.values.yaml --set service.type=$SERVICE_TYPE
    elif [[ $2 == *"star-claims"* ]]; then
        helm upgrade star-claims k8s/api-bank -f k8s/star.claims.values.yaml --set service.type=$SERVICE_TYPE
    fi
fi

# watch kubectl get pods
# watch -n 10 'kubectl get services --no-headers | awk "{print \$1, \$4}"'