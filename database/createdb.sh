#!/bin/bash

mongoimport --db smartcity --collection advertisings --file advertisings.json --jsonArray --drop
mongoimport --db smartcity --collection agenda --file agenda.json --jsonArray --drop
mongoimport --db smartcity --collection interests --file interests.json --jsonArray --drop
mongoimport --db smartcity --collection networks --file networks.json --jsonArray --drop
mongoimport --db smartcity --collection offers --file offers.json --jsonArray --drop
mongoimport --db smartcity --collection services --file services.json --jsonArray --drop
mongoimport --db smartcity --collection towns --file towns.json --jsonArray --drop
mongoimport --db smartcity --collection trades --file trades.json --jsonArray --drop
mongoimport --db smartcity --collection trade_types --file trade_types.json --jsonArray --drop
mongoimport --db smartcity --collection users --file users.json --jsonArray --drop
