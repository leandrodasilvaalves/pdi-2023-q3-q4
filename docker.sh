#!/bin/bash

docker-compose down
if [[ $1 == *"-b"* ]]; then
    docker-compose up --build -d
    echo removing intermediate images
    docker image rm -f $(docker image ls --format "{{.ID}} {{.Repository}}:{{.Tag}}" | grep -i '<none>' | awk '{print $1}')
fi

i=0
max=12

while [ $i -lt $max ]
do
    docker-compose up -d
    i=$((i + 1))
    echo "Attempt $i of $max"
    sleep $i
done

docker-compose logs --follow --tail=all star-accounts