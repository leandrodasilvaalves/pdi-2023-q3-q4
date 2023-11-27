#!/bin/bash
# MONGO_INITDB_ROOT_USERNAME=root
# MONGO_INITDB_ROOT_PASSWORD=root123Mudar

mongosh -u $MONGO_INITDB_ROOT_USERNAME -p $MONGO_INITDB_ROOT_PASSWORD <<EOF
use bacen
db.dropDatabase()

use vulture
db.dropDatabase()

use star
db.dropDatabase()

show dbs
EOF

# docker-compose exec mongo bash -c "/scripts/clean-mongo.sh"
# kubectl exec --tty -i mongo-mongodb-6bf879d49f-5gz9z --namespace default -- bash
