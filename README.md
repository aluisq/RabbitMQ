# Tutorial

[RabbitMQ - 2021](https://www.luisdev.com.br/2021/01/17/mensageria-com-rabbitmq-e-asp-net-core-parte-1/)

[RabbitMQ - 2022](https://www.luisdev.com.br/2023/01/01/implementando-publish-subscribe-com-asp-net-core-rabbitmq-e-docker/)


# Começando...

## Baixa a imagem do RabbitMQ
    - docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
    - http://localhost:15672/
    - usuario e senha: guest

## Configurando as Filas
- Primeiramente, na UI do RabbitMQ, navegue para a aba “Exchanges”. Expanda a sessão “Add a new exchange”, e preencha com os seguintes dados.
Name: customers-service
Type: topic
- Clique em “Add exchange”
- Navegue para a aba “Queues”
- Expanda a sessão “Add a new queue”, e preencha com os seguintes dados:
Name: notifications-service/customer-created
Type: Classic
- Clique em “Add queue”
- Repita o processo para os seguintes dados:
Name: sales-service/customer-created
Type: Classic
Acesse a fila criada “notifications-service/customer-created”, expanda a aba “Bindings” e preencha os seguintes dados:
From exchange: customers-service
Routing key: customer-created
- Repita o processo anterior, mas dessa vez para a fila “sales-service/customer-created”, com os mesmos dados.

