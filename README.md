# Ticket Now
Plataforma de venda de ingressos para eventos, feito em .NET 7

### 📋 Pré-requisitos

* .NET 7 (https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
* Docker (https://www.docker.com/products/docker-desktop/)

## Integrantes

[Lucas Hanke](https://github.com/lucasbagrt)
[João Gasparini](https://github.com/joaogasparini)
[Victoria Pacheco](https://github.com/vickypacheco)
[Rafael Araujo](https://github.com/RafAraujo)
[Cristian Kulessa](https://github.com/Kulessa)

## Build 

Para rodar este projeto, siga estes passos

* Crie o banco de dados no docker, com o comando abaixo
* Execute a API do TicketNow
* Execute a API do MockPayment

### Database

Este projeto usa o SQL Server, você pode usar uma instância instalada em sua área de trabalho ou pode usar o Docker para criar. As etapas que mostraremos aqui são usando Docker.

```docker
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=1q2w3e4r@#$' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

### TicketNow Api

Essa API foi desenvolvida em .NET 7, com autenticação em JWT, e banco de dados SQL Server

Credenciais:
User: admin
Password: 1q2w3e4r@#$

Endpoints

#Order:
![image](https://github.com/Kulessa/Fiap2NettTC1/assets/60990141/be3a03e7-bf08-4898-9b20-b28f2a874de6)




### MockPayment

Essa API foi desenvolvida em .NET 7, utilizando Minimal API, com o banco de dados SQL Server,
está API é utilizada apenas internamente para mockarmos os pagamentos.

**Ao finalizar um Pedido (Order):**

Caso a forma de pagamento for cartão, existe uma probabilidade de 50% de retornar transação não autorizada (Unauthorized),
e 50% de retornar sucesso.

Caso a forma de pagamento for Pix ou Boleto, processamos ela internamente a cada 2 minutos, e tambem existe uma chance de 50% de retornar Expired (como se o cliente não tivesse pago o pix ou boleto),
e 50% de retornar sucesso.

Todos os retornos dessa API, são feitos via webhook.

Endpoints (Usados apenas internamente)

![image](https://github.com/Kulessa/Fiap2NettTC1/assets/60990141/cdb5edf4-7c1d-4d31-9f05-28b97b9888b0)

