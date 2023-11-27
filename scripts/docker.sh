#!/bin/bash

docker-compose down
if [[ $1 == *"-b"* ]]; then
  docker-compose up --build -d
  echo removing intermediate images
  docker image rm -f $(docker image ls --format "{{.ID}} {{.Repository}}:{{.Tag}}" | grep -i '<none>' | awk '{print $1}')
fi

docker-compose up -d
docker-compose logs --follow --tail=all bacen star-entries star-accounts star-claims vulture
