#!/bin/bash

mongosh -u $MONGO_INITDB_ROOT_USERNAME -p $MONGO_INITDB_ROOT_PASSWORD  <<EOF
use bacen
db.dropDatabase()

use vulture
db.dropDatabase()

use star
db.dropDatabase()

show dbs
EOF


# docker-compose exec mongo bash -c "/scripts/clean-mongo.sh"
