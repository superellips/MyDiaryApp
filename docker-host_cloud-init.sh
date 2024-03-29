#!/bin/bash

# Inserts go here



# End inserts

# Install docker
function installDocker () {
    apt-get update -y && apt-get install -y ca-certificates curl
    install -m 0755 -d /etc/apt/keyrings
    curl -fsSL https://download.docker.com/linux/ubuntu/gpg -o /etc/apt/keyrings/docker.asc
    chmod a+r /etc/apt/keyrings/docker.asc
    echo \
    "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.asc] https://download.docker.com/linux/ubuntu \
    $(. /etc/os-release && echo "$VERSION_CODENAME") stable" | \
    tee /etc/apt/sources.list.d/docker.list > /dev/null
    apt-get update -y
    apt-get install -y docker-ce docker-ce-cli containerd.io docker-buildx-plugin docker-compose-plugin
}

# Setup selfhosted runner for GitHub actions
function setupRunner () {
    mkdir /home/$admin_user/actions-runner
    cd /home/$admin_user/actions-runner
    curl -o actions-runner-linux-x64-2.314.1.tar.gz -L https://github.com/actions/runner/releases/download/v2.314.1/actions-runner-linux-x64-2.314.1.tar.gz      
    tar xzf ./actions-runner-linux-x64-2.314.1.tar.gz
    chown -R $admin_user:$admin_user ../actions-runner
    sudo -u $admin_user ./config.sh --unattended --replace --url https://github.com/$gh_user/$app_name --token $token
    ./svc.sh install $admin_user
    ./svc.sh start
}

# Make containers
function makeContainers () {
    # This will fetch the image for my application
    docker run -d -p 80:8080 --name $app_name stjarnstoft/${app_name,,}
}

export DEBIAN_FRONTEND=noninteractive

installDocker
setupRunner
makeContainers
