FROM mcr.microsoft.com/devcontainers/dotnet:9.0-jammy
# Install the libleveldb-dev package 
RUN apt-get update && apt-get install -y libleveldb-dev
