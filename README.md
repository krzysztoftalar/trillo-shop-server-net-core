<h1 align="center">Trillo Shop Server</h1>

<p align="center">
<img src="https://img.shields.io/badge/made%20by-krzysztoftalar-blue.svg" />

<img src="https://img.shields.io/badge/.NET%20Core-3.1.0-blueviolet" />

<img src="https://img.shields.io/badge/license-MIT-green" />
</p>

## About The Project

_This is the server-side of the Trillo Shop application. The project is under construction..._

_Go to **[Trillo Shop Client](https://github.com/krzysztoftalar/trillo-shop-client-react-redux)**._

_Go to **[Trillo Shop UML diagrams](https://github.com/krzysztoftalar/trillo-shop-server-net-core/blob/master/trillo_uml.pdf)**._

## Features

- Shopping cart management
- Creating an order
- Payment with Stripe
- Login

## Built With

| Server  
| ----------------------------------------------------------------------------------------------------------------
| [.NET Core](https://docs.microsoft.com/en-us/dotnet/) 3.1.0 |
| [ASP.NET Web API](https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-3.1) |
| [Entity Framework](https://docs.microsoft.com/en-us/ef/) |
| [MediatR](https://github.com/jbogard/MediatR/wiki) |
| [Swagger](https://swagger.io/)
| [Stripe](https://stripe.com/en-pl)

## Getting Started

### Prerequisites

- .NET Core 3.1.0
- SQL Server

### Installation

1. **Create an account on Stripe.**
2. **In solution WebUI in `appsettings.json` set your Stripe account details, database connection string and client url.**

```JSON
"Stripe": {
    "WebHook": "ENTER YOUR ACCOUNT DETAIL",
    "SecretKey": "ENTER YOUR ACCOUNT DETAIL",
    "PublicKey": "ENTER YOUR ACCOUNT DETAIL",
    "Domain": "ENTER YOUR CLIENT URL",
  }
```

```JSON
"Routing": {
    "Client": "ENTER YOUR CLIENT URL"
  }
```

```JSON
"ConnectionStrings": {
    "EFTrilloShop": "ENTER YOUR CONNECTION STRING"
  },
```

3. **For Stripe webhook testing run the following command in the root of the repository.**

```shell
stripe listen --forward-to https://localhost:5001/api/payments/webhook
```

4. **Build and run the solution.**

## License

This project is licensed under the MIT License.

## Contact

**Krzysztof Talar** - [Linkedin](https://www.linkedin.com/in/ktalar/) - krzysztoftalar@protonmail.com
