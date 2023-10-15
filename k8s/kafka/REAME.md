To create a pod that you can use as a Kafka client run the following commands:

    ```sh
    kubectl run kafka-client --restart='Never' --image docker.io/bitnami/kafka:3.5.1-debian-11-r72 --namespace default --command -- sleep infinity
    ```

    ```sh
    kubectl cp --namespace default ./client.properties kafka-client:/tmp/client.properties
    ```
    ```sh
    kubectl exec --tty -i kafka-client --namespace default -- bash
    ```

    ```sh
    # PRODUCER:
        kafka-console-producer.sh \
            --producer.config /tmp/client.properties \
            --broker-list kafka-controller-0.kafka-controller-headless.default.svc.cluster.local:9092 \
            --topic test
    ```

    ```sh       
    # CONSUMER:
        kafka-console-consumer.sh \
            --consumer.config /tmp/client.properties \
            --bootstrap-server kafka.default.svc.cluster.local:9092 \
            --topic test \
            --from-beginning
    ```