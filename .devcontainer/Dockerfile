# Grabs VS Code's devcontainer base image for rust
FROM mcr.microsoft.com/devcontainers/rust:1-1-bullseye

# Copy library scripts to execute
COPY overlay/*.sh /tmp/overlay/

# Install necessary ubuntu package dependencies
RUN /tmp/overlay/install-packages.sh