#!/bin/bash

app_name=MyDiaryApp
rg_name=$app_name-RG
dockerhost_name=$app_name-Host
admin_user=azureuser
location=swedencentral

gh_user=superellips
cloud_init=$(mktemp)
trap "rm -f $cloud_init" EXIT

# Get the registration token for the self-hosted runner
function getRunnerToken () {
  local gh_token_response=$(gh api --method POST \
    -H "Accept: application/vnd.github+json" \
    -H "X-GitHub-Api-Version: 2022-11-28" \
    repos/$gh_user/$app_name/actions/runners/registration-token)

  echo $(echo $gh_token_response | jq -r '.token')
}

# Get a temporary cloud init file for the Docker Host
function getCloudInit () {
  cat docker-host_cloud-init.sh > $cloud_init
  sed -i "5i token=$(getRunnerToken)" $cloud_init
  sed -i "5i app_name=$app_name" $cloud_init
  sed -i "5i gh_user=$gh_user" $cloud_init
  sed -i "5i admin_user=$admin_user" $cloud_init
  echo $cloud_init
}

# Provisions the clouds environment
function provisionEnvironment () {
    local script=$(getCloudInit)

    az group create -n $rg_name -l $location

    az vm create -g $rg_name -n $dockerhost_name \
        --size Standard_B1s --image Ubuntu2204 \
        --generate-ssh-keys --admin-username $admin_user \
        --custom-data @$script

    az vm open-port -g $rg_name -n $dockerhost_name --port 80
}

provisionEnvironment

public_ip=$(az vm show -g $rg_name -n $dockerhost_name \
    --show-details --query [publicIps] --output tsv)

echo "RG-Name: $rg_name"
echo "IP: $public_ip"
