﻿# GatewayTesting
### DEVS
* Jens
* Andreas
* Emil

## Purpose
This Projet is a school Exams project at EASV(erhvervsakademi sydvest | business academy southwest). \
This project where made for purely educational purposes and should not be used for any monetary gains. \
It was designed for different gateways research

## How to use Kong config
run this command "kong config db_import /etc/kong/KongConfig.yml" on kongservice, you can use docker desktop or another terminal to access the container to import the configuration form the config file.
after importing restart the kongservice and then you need to add apikey to your request headers