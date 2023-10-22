kubectl run --restart='Never' topics-creator-pod --image=confluentinc/cp-zookeeper:latest -- sleep infinity
kubectl cp --namespace default k8s/kafka/client.properties topics-creator-pod:/tmp/client.properties

kubectl exec --tty -i topics-creator-pod -- bash - <<EOF

kafka-topics --bootstrap-server kafka.default.svc.cluster.local:9092 --create --if-not-exists --topic bacen.entries --replication-factor 1 --partitions 1
kafka-topics --bootstrap-server kafka.default.svc.cluster.local:9092 --create --if-not-exists --topic bacen.claims --replication-factor 1 --partitions 1

kafka-topics --bootstrap-server kafka.default.svc.cluster.local:9092 --create --if-not-exists --topic vulture.entries --replication-factor 1 --partitions 1
kafka-topics --bootstrap-server kafka.default.svc.cluster.local:9092 --create --if-not-exists --topic vulture.claims --replication-factor 1 --partitions 1

kafka-topics --bootstrap-server kafka.default.svc.cluster.local:9092 --create --if-not-exists --topic star.entries --replication-factor 1 --partitions 1
kafka-topics --bootstrap-server kafka.default.svc.cluster.local:9092 --create --if-not-exists --topic star.claims --replication-factor 1 --partitions 1

echo -e 'Successfully created the following topics:'
kafka-topics --bootstrap-server kafka.default.svc.cluster.local:9092 --list

EOF

kubectl delete pod topics-creator-pod