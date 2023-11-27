#!/bin/sh

topics="bacen.entries bacen.claims"

for topic in $topics; do
  echo "Checking topic $topic"
  kafka-topics --bootstrap-server localhost:9092 --describe --topic "$topic" >/dev/null 2>&1
  if [ $? -ne 0 ]; then
    echo "Topic $topic does not exist" && exit 1
  fi
done
echo "Topics exist. kafka is healthy" && exit 0
