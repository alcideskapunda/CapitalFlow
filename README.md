# CapitalFlow - Sistema de Compra Programada de Ações

[![Development Deploy](https://github.com/alcideskapunda/CapitalFlow/actions/workflows/dev-deploy.yml/badge.svg?branch=dev)](https://github.com/alcideskapunda/CapitalFlow/actions/workflows/dev-deploy.yml)

## CapitalFlow API

Rest API for **CapitalFlow** Scheduled Stock Purchase System, developed by [Alcides Pedro Capunda dos Santos](https://www.linkedin.com/in/alcideskapunda22/).

The API allows clients to join a recurring and automated investment plan in the "Top Five" recommended portfolio, managing consolidated purchases, fractional distributions, rebalancing, and tax obligations.

---

## Table of contents

- [Overview](#overview)
- [Applied market concepts](#Applied-market-concepts)
- [Tech stack](#tech-stack)
- [Solution structure](#solution-structure)
- [Architecture](#architecture)
- [Getting started](#getting-started)
- [Configuration](#configuration)
- [Running the API](#running-the-api)
- [Docker](#docker)
- [Database and migrations](#database-and-migrations)
- [API documentation](#api-documentation)
- [CI/CD](#cicd)
- [Naming conventions](#naming-conventions)
- [License](#license)

---

## Overview

The backend provides HTTP APIs for:

- User management (admins and customers)
- JWT-based authentication and authorization
- Automating monthly client contributions by dividing them into 3 installments (days 5, 15 and 25)
- O motor do sistema:
  - It groups the capital of active clients.
  - It makes the purchase in a Master account of the brokerage firm (using quotes from the B3 COTAHIST archive).
  - It distributes the shares proportionally to the individual accounts (Subsidiary accounts).
  - It retains the rounding residues in the Master account for the next cycle.
- Domain rules organized for maintainability and scale

---

## Applied market concepts

| Concept                         | System Application                                                                                             |
| ------------------------------- | -------------------------------------------------------------------------------------------------------------- |
| **Standard vs Fractional Lot**  | Standard purchases use the default ticker. Quantities from 1 to 99 use the "F" suffix (e.g., `PETR4F`).        |
| **"Dedo-Duro" Income Tax**      | Automatic retention of 0.005% over the value of each distributed operation (published via Kafka).              |
| **Tax Exemption (Individuals)** | 20% tax over net profit is exempted if total sales in the month do not exceed R$ 20,000.00.                    |
| **Average Acquisition Price**   | Continuous weighted average calculation. Sales only decrease the quantity and do not change the average price. |
| **B3 COTAHIST File**            | Daily reading and parsing of official B3 closing quotes from `.TXT` files.                                     |

---

## Tech stack

| Technology                           | Notes                                                             |
| ------------------------------------ | ----------------------------------------------------------------- |
| **.NET 10**                          | Target framework (`net10.0`)                                      |
| **FastEndpoints**                    | HTTP API layer (see `Program.cs`)                                 |
| **Entity Framework Core 9**          | Data access                                                       |
| **Pomelo.EntityFrameworkCore.MySql** | MySQL provider                                                    |
| **MySQL 8+**                         | Database                                                          |
| **Apache Kafka**                     | Messaging system for IR events (running in Docker)                |
| **JWT**                              | Bearer authentication (`FastEndpoints.Security`)                  |
| **Serilog**                          | Structured logging                                                |
| **Hangfire**                         | Background and recurring jobs                                     |
| **Scalar**                           | API reference UI (Development only)                               |
| **Docker**                           | Container image (see root `Dockerfile`)                           |
| **COTAHIST Parser**                  | Customized component for reading files in the folder `cotacoes/`. |

Patterns used: **vertical slice** organization, **CQRS**-style commands/queries, **REPR** (Request–Endpoint–Response), **FastEndpoints in-process events** (`IEvent` / `IEventHandler<T>`), and clean separation across projects.

---

## Solution structure

| Folder                | Project                        | Responsibility                                                                         |
| --------------------- | ------------------------------ | -------------------------------------------------------------------------------------- |
| -- cotacoes/          | ---                            | B3 COTAHIST files (.TXT)                                                               |
| -- src/               | ---                            | System source code                                                                     |
|                       | **CapitalFlow.Api**            | Host, FastEndpoints, feature slices under `Features/`, `Program.cs`, API configuration |
|                       | **CapitalFlow.Domain**         | Domain entities and domain-focused types                                               |
|                       | **CapitalFlow.Application**    | Application abstractions (e.g. B3QuoteParser)                                          |
|                       | **CapitalFlow.Persistence**    | `CapitalFlowDbContext`, EF configurations, repositories, **Migrations/**, seeding      |
|                       | **CapitalFlow.Infrastructure** | Infrastructure implementations (e.g. JWT, Hangfire jobs), `DependencyInjection.cs`     |
| -- tests/ ---         | ---                            |
|                       | **CapitalFlow.Tests**          | Testing (Minimum 70% coverage)                                                         |
| -- docker-compose.yml | ---                            | Orchestration of Kafka and MySQL                                                       |

---

## Architecture

```
                    [Administrador]

                          |
                    [Cesta Top Five]
                    (5 ações + %)
                          |
[Cliente] ---adesão---> [Conta Gráfica Filhote] ---> [Custódia Filhote]
                                                          ^

                                                          |
                                                    (distribuição)
                                                          |
                          [Conta Master] ----------> [Custódia Master]

                                |
                          (compra consolidada)
                                |
                          [Arquivo COTAHIST B3]
```

Feature code lives under **`src/CapitalFlow.Api/Features/`** using **nested** folders, for example:

- `Features/RecommendationBasket/UpsertRecommendationBasket/`
- `Features/Users/GetUsers/`

A typical slice includes an endpoint class, command or query, handler, optional FluentValidation validator, and optional domain events under `Events/`.

---

## Getting started

### Prerequisites

- [.NET SDK 10](https://dotnet.microsoft.com/download)
- **MySQL 8+** (local instance or remote) with a database for the app
- **Docker** (for containerized run/build)

### Clone

```bash
git clone https://github.com/alcideskapunda/CapitalFlow.git
cd CapitalFlow
```

### Restore and build

```bash
dotnet restore CapitalFlow.sln
dotnet build CapitalFlow.sln
```

---

## Configuration

Base files:

- `appsettings.json` — minimal defaults
- `appsettings.Development.json` / `appsettings.Production.json` — logging and non-secret tuning where applicable

---

## Running the API

```bash
docker-compose up -d
```

```bash
dotnet run --project src/CapitalFlow.Api/CapitalFlow.Api.csproj
```

Default URLs from `Properties/launchSettings.json`:

- HTTP: `http://localhost:5230`
- HTTPS profile: `https://localhost:7178` and `http://localhost:5230`

Set `ASPNETCORE_ENVIRONMENT` to `Development` to enable OpenAPI and Scalar (see below).

---

## Docker

Build from the repository root:

```bash
docker build -t capitalflow-api .
```

The image sets **`ASPNETCORE_HTTP_PORTS=3333`** and exposes port **3333**.

---

## Database and migrations

- EF Core migrations are in **`src/CapitalFlow.Persistence/Migrations/`**.
- The context is **`CapitalFlowDbContext`**.
- Add migrations from the solution root (example; adjust startup project if your tooling requires it):

  ```bash
  dotnet ef migrations add <Name> --project src/CapitalFlow.Persistence --startup-project src/CapitalFlow.Api
  ```

- Apply updates with `dotnet ef database update` using the same `--project` / `--startup-project` pair.

---

## API documentation

In the **Development** environment only, the app exposes an OpenAPI document via **FastEndpoints.Swagger** (NSwag) and **Scalar**:

- OpenAPI JSON: **`/openapi/v1.json`** (NSwag; includes FastEndpoints metadata such as `Summary` parameter descriptions)
- Scalar UI: **`/scalar`** (or **`/scalar/v1`** depending on routing)

Interactive docs rely on **`FastEndpoints.Swagger`**, not `Microsoft.AspNetCore.OpenApi`, so per-parameter descriptions use `Summary(s => s.Params[...] = "...")` or XML docs on request DTOs (see [FastEndpoints Swagger docs](https://fast-endpoints.com/docs/swagger-support)).

Production builds do not map these endpoints by default (`Program.cs`).

---

## CI/CD

The workflow **[`.github/workflows/dev-deploy.yml`](.github/workflows/dev-deploy.yml)** runs on pushes to **`dev`**:

1. Builds and pushes a Docker image to the registry configured in repository secrets.

Required secrets (names referenced in the workflow) include Docker registry credentials and image repository.

---

## Naming conventions

Align new code with existing slices in the same feature area.

| Kind            | Pattern                            | Example                                   |
| --------------- | ---------------------------------- | ----------------------------------------- |
| Endpoint        | `[Action][Entity]Endpoint`         | `RegisterUserEndpoint`                    |
| Query           | `Get[Entity][OptionalFilter]Query` | `GetUsersQuery`                           |
| Query handler   | `[QueryName]Handler`               | `GetUsersQueryHandler`                    |
| Command         | `[Action][Entity]Command`          | `UpsertRecommendationBasketCommon`        |
| Command handler | `[CommandName]Handler`             | `UpsertRecommendationBasketCommonHandler` |
| Validator       | `[CommandName]Validator`           | `RegisterUserCommandValidator`            |

Prefer consistency with nearby files; avoid introducing alternate naming styles in the same feature folder.

---

## License

This project is licensed under the **MIT License** — see the file [LICENSE](./LICENSE) For more details.

> ⚠️ **Note on Business Rules:** : The ideas and business logic described in this document are based on a technical challenge proposed by a large financial institution. This repository contains only a practical implementation carried out by me for technical evaluation and personal portfolio purposes, without any commercial ties or rights to the product concept.

---

<div align="center">

Developed by [Alcides Pedro Capunda dos Santos](https://www.linkedin.com/in/alcideskapunda22/) and [Ântero Kapunda]()

</div>
