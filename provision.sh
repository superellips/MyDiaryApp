#!/bin/bash

app_name=MyDiaryApp
rg_name=$app_name-RG
dockerhost_name=$app_name-Host
admin_username=azureuser
location=swedencentral

az group create -n $rg_name -l $location

az vm create -g $rg_name -n $dockerhost_name \
    --size Standard_B1s --image Ubuntu2204 \
    --generate-ssh-keys --admin-username $admin_username \
    --custom-data @docker-host_cloud-init.sh

az vm open-port -g $rg_name -n $dockerhost_name --port 80

public_ip=$(az vm show -g $rg_name -n $dockerhost_name \
    --show-details --query [publicIps] --output tsv)

echo "RG-Name: $rg_name"
echo "IP: $public_ip"
