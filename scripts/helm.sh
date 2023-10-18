#!/bin/bash

if [[ $1 == *"-i"* ]]; then
    helm install kafka bitnami/kafka -f ./k8s/kafka/values.yaml
    helm install kakfa-ui kafka-ui/kafka-ui -f ./k8s/kafka/ui/values.yaml
    helm install mongo bitnami/mongodb -f ./k8s/mongo/values.yaml
    helm install mongo-express ./k8s/mongo/ui/   
    helm install bacen ./k8s/api-bank 
fi

if [[ $1 == *"-x"* ]]; then
    helm uninstall kafka
    helm uninstall kakfa-ui
    helm uninstall mongo 
    helm uninstall mongo-express
    helm uninstall bacen
fi

if [[ $1 == *"-u"* ]]; then
    if [[ $2 == *"kafka"* ]]; then
        helm upgrade kafka -f ./k8s/kafka/values.yaml
    elif [[ $2 == *"kafka-ui"* ]]; then
        helm upgrade kafka-ui -f ./k8s/kafka/ui/values.yaml
    elif [[ $2 == *"mongo"* ]]; then
        helm upgrade mongo -f ./k8s/mongo/values.yaml
    elif [[ $2 == *"mongo-express"* ]]; then
        helm upgrade mongo-express ./k8s/mongo/ui
    elif [[ $2 == *"bacen"* ]]; then 
        helm upgrade bacen ./k8s/api-bank
    fi
fi

# watch kubectl get pods
# watch -n 10 'kubectl get services --no-headers | awk "{print \$1, \$4}"'