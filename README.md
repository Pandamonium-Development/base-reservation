# 🏨 Base Reservation System

**Base Reservation** is a modular and extensible reservation system built to serve as a foundation for client-specific booking platforms. It follows clean architecture principles and includes backend APIs, an admin dashboard, and reusable development snippets.

---

## 📐 Architecture Overview

The solution is structured around separation of concerns and modularity. Below is an overview of each key part:

```
base-reservation/
├── api/                     # ASP.NET Core backend API
│   ├── BaseReservation.WebAPI/              # API endpoints, controllers, filters
│   ├── BaseReservation.Application/         # Use cases, DTOs, interfaces
│   ├── BaseReservation.Domain/              # Entities and aggregates
│   ├── BaseReservation.Infrastructure/      # Data access, external services
│   ├── BaseReservation.Common/              # Shared logic and helpers
│   ├── BaseReservation.Database/            # EF Core context and migrations
│   ├── BaseReservation.Utils/               # Utility functions
│   └── BaseReservation.Tests/               # xUnit test projects
│
├── site-admin/              # React.js-based admin interface
│
├── snippets/                # Reusable code snippets (e.g., validations, deployments)
└── README.md                # Documentation
```

Key architectural patterns:
- **Clean Architecture**: Business logic is isolated from framework dependencies.
- **Dependency Injection**: Inversion of control through constructor injection.
- **DTO Mapping**: Requests/responses use specific data transfer objects.
- **Swagger/OpenAPI**: Automatically generated API docs.

---


## 🚀 Features

- Clean architecture with separation of concerns
- ASP.NET Core Web API with x-api-version support
- Entity Framework Core
- FluentValidation
- AutoMapper
- Unit of Work and generic repository
- Swagger/OpenAPI with versioned docs
- CI/CD pipeline with GitHub Actions (TBD)
- Environment-based configuration
- Ready for Docker deployment (TBD)
- Future-ready: admin and public frontends

## 🚀 Setup Instructions

### Prerequisites

- [.NET SDK 9](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Node.js 22+](https://nodejs.org/)
- [Yarn](https://yarnpkg.com/) or [Npm](https://docs.npmjs.com/downloading-and-installing-node-js-and-npm) (optional)
- [Docker](https://www.docker.com/) (optional, for DB)

### API Setup

- [Go to instructions for API]()

### Admin Site Setup

- [Go to instructions for Admin Site]()

---


## 📘 Swagger Documentation

When running the API, navigate to:

```
https://localhost:5001/swagger/index.html
```

This is automatically generated using [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore).

You can access:
- Available endpoints
- Sample requests/responses
- API versioning (via `x-api-version` header)

---

## 🧪 Unit Testing

To run tests:

- [Go to instructions for API]()
- [Go to instructions for Admin Site]()
---

## 🌐 Live Deployment (Optional)

Use your VPS or cloud platform (e.g., DigitalOcean, AWS) and follow this flow:

1. Setup `systemd` services for API and admin.
2. Reverse proxy with NGINX.
3. Auto-deploy via GitHub Actions (`ci-cd.yml` under `.github/workflows/`).
4. Handle backups and health checks with Bash scripts under `snippets/deploy`.

---

## 🌐 Live Demo

Coming soon!
