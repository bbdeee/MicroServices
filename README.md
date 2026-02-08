# Microservices & Containers – Team Project

This repository contains my contribution to a team-based learning project where the main goal was to understand how containerized services work together using Docker, Kubernetes, and RabbitMQ.

The focus of the project was not on writing a lot of application code, but on connecting services, configuring infrastructure, and seeing how everything behaves in a distributed environment.

This service was developed as part of a larger microservices system that was assembled and run together with other services.

## What’s in this repository

- A small .NET Web API (profile/login-related microservice)
- Dockerfile for building the service as a container
- Kubernetes YAML files for:
  - deploying the service
  - exposing it through a Kubernetes Service
- Application configuration and basic logging
- Simple HTTP requests for testing endpoints

## How it was used

The typical workflow was:
- build the service as a Docker image
- deploy the service into a Kubernetes cluster
- run it together with other services (including RabbitMQ and an API gateway)
- test communication between services and endpoints

## What I learned

- How Docker images and containers differ from running apps locally
- How Kubernetes Deployments and Services work together
- Basic service networking and routing in a cluster
- How message brokers like RabbitMQ fit into a microservices setup
- That most complexity in microservices comes from integration and configuration rather than application code

## Collaboration

This project was developed in a group setting. Individual microservices were developed separately, and the full system was assembled and tested together to focus on integration, orchestration, and infrastructure concepts.

This repository represents my part of the system and how it connected to the larger project.

## Related Team Project (Full System)

The full microservices system, including additional services, gateway configuration, and RabbitMQ deployment, can be found here:

**Microservices & Containers – Team Project**  
Docker, Kubernetes, RabbitMQ

https://github.com/JohnLjungblad/microservice_1

