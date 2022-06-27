# Webhooks Service Api v1.0.0

Webhooks Api contains such Events / Subscriptions / Webhooks APIs:

**Event APIs:**

- POST:
/api/v{version}/Events

- GET:
/api/v{version}/Events

- PUT:
/api/v{version}/Events/{eventId}

- DELETE: 
/api/v{version}/Events/{eventId}

- GET:
/api/v{version}/Events/{eventId}

**Subscriptions APIs:**

- POST:
/api/v{version}/Subscriptions

- GET:
/api/v{version}/Subscriptions

- PUT:
/api/v{version}/Subscriptions/{subscriptionId}

- DELETE:
/api/v{version}/Subscriptions/{subscriptionId}

- GET:
/api/v{version}/Subscriptions/{subscriptionId}

**Webhooks APIs:**

- POST:
/api/v{version}/Webhooks

- GET:
/api/v{version}/Webhooks

- PUT:
/api/v{version}/Webhooks/{webhookId}

- DELETE:
/api/v{version}/Webhooks/{webhookId}

- GET:
/api/v{version}/Webhooks/{webhookId}

# Hosting

Host Name: 
http://webhooks-service-api.westeurope.cloudapp.azure.com/

Swagger UI: 
http://webhooks-service-api.westeurope.cloudapp.azure.com/swagger

# Technological Stack:

- .NET6 / C#

- REST

- MS Azure

- Azure Kubernetes

- Docker

- Application Insights

- RabbitMQ (https://www.cloudamqp.com/)

- MongoDB / CosmoDB

- GitHub / Git

- Swagger

- Visual Studio 2022 / Robo 3T 1.4.3 / MongoDb Compass

# Deployment

[Create an image pull secret:](https://docs.microsoft.com/en-us/azure/container-registry/container-registry-auth-kubernetes)

```
kubectl create secret docker-registry <secret-name> \
    --namespace <namespace> \
    --docker-server=<container-registry-name>.azurecr.io \
    --docker-username=<service-principal-ID> \
    --docker-password=<service-principal-password>
```

YAML:
https://github.com/valentindudnik/webhooks.service.api/tree/main/yaml/webhooks-service-api.yml

# NuGet Packages:

[Webhooks.RabbitMQ.Client v1.0.0](https://www.nuget.org/packages/Webhooks.RabbitMQ.Client/1.0.0?_src=template)

[Webhooks.RabbitMQ.Models v1.0.0](https://www.nuget.org/packages/Webhooks.RabbitMQ.Models/1.0.0?_src=template)

GitHub Repository:
https://github.com/valentindudnik/webhooks.rabbitmq
