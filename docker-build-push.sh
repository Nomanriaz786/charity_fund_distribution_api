#!/bin/bash

# Set your Docker Hub username
export DOCKERHUB_USERNAME="nomanriaz"

# Ensure you're logged into Docker Hub
docker login

# Build the images with the correct tag
docker-compose build

# Push to Docker Hub
docker-compose push charity-webapi

echo "Successfully built and pushed images to Docker Hub"
