#!/usr/bin/env -S bash -e

export DEBIAN_FRONTEND=noninteractive
export DISTRO=$(lsb_release -is | tr 'A-Z' 'a-z')
export CODENAME=$(lsb_release --codename --short)

# Install build and debug dependencies
#
sudo apt-get update
sudo apt-get install -y \
    mingw-w64 \
    gdb

# Install Apache Arrow
#
sudo apt update
sudo apt install -y -V \
    ca-certificates \
    lsb-release wget

wget https://apache.jfrog.io/artifactory/arrow/${DISTRO}/apache-arrow-apt-source-latest-${CODENAME}.deb
sudo apt install -y -V ./apache-arrow-apt-source-latest-${CODENAME}.deb
sudo apt update
sudo apt install -y -V libarrow-glib-dev
rm -rf ./apache-arrow-apt-source-latest-${CODENAME}.deb
