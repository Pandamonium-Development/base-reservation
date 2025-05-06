# Base Reservation API

This project is a backend API for managing reservations. It is built using .NET Core and follows best practices for scalability and maintainability.

## 📁 Project Structure
The solution is organized into multiple projects, each encapsulating a specific concern:​

```bash
api
├── BaseReservation.Application    # Contains application logic, including service interfaces and implementations.
├── BaseReservation.Common         # Houses shared utilities, constants, and helper classes.
├── BaseReservation.Database       # SQL files, clean query and populate data
├── BaseReservation.Domain         # Defines domain entities and aggregates, adhering to DDD principles.
├── BaseReservation.Infrastructure # Implements data access layers, external service integrations, and repository patterns.
├── BaseReservation.Tests          # Contains unit and integration tests to ensure code reliability.​
├── BaseReservation.Util           # Provides additional utility functions and extensions.
├── BaseReservation.WebAPI         # Hosts the ASP.NET Core Web API controllers and middleware configurations.
````

This modular architecture promotes separation of concerns, testability, and maintainability.​

## 🚀 Features
- Clean Architecture: Adheres to Clean Architecture principles, facilitating a clear separation between business logic and infrastructure.

- Entity Framework Core: Utilizes EF Core for ORM, enabling efficient database interactions and migrations.

- Dependency Injection: Leverages built-in DI for managing service lifetimes and dependencies.

- Asynchronous Programming: Implements async/await patterns for non-blocking operations.

- Comprehensive Testing: Includes a dedicated testing project to ensure code quality and reliability.​

---

## Setup

### Prerequisites
Before starting, ensure you have the following installed:
- [.NET SDK 9](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Docker](https://www.docker.com/) and Docker Compose
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (if not using Docker for the database)

#### Configuration on docker (optional)
1. Navigate to `infra` folder
```bash
cd ./infra
```
2. Follow instructions in the docker-installation-guide.txt

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/Pandamonium-Development/base-reservation.git
   cd base-reservation/api
   ````
2. Restore dependencies
```bash
dotnet restore
```

3. Apply database migrations:

Make sure to add the connection string in you appsettings.json or secrets file

```bash
"ConnectionStrings": {
    "BaseReservationDatabase": ""
},
```
or 
```bash
"ConnectionStrings:BaseReservationDatabase": "Data Source=localhost;Initial Catalog=BaseReservation;Persist Security Info=True;User ID=sa;Password=\"{saPassword}\";TrustServerCertificate=True",
```
** Replace `{saPassword}` with the password you configure

```bash
cd ./base-reservation/api/BaseReservation.WebAPI
dotnet ef database update -p ../BaseReservation.Infrastructure/BaseReservation.Infrastructure.csproj
```

4. Run the API:

Before running make sure to complete the appsettings.json or secrets file with this configuration properties

```bash
"AuthenticationConfiguration": {
    "JwtSettings": {
      "Secret": "",
      "TokenLifetime": "00:15:00"
    }
}
```
or
```bash
"AuthenticationConfiguration:JwtSettings_Secret": "{JWT_Secret}",
"AuthenticationConfiguration:JwtSettings_TokenLifetime": "00:15:00"
```

```bash
dotnet run --project BaseReservation.WebAPI
```

** This can also be executed with run and debug option from vs code or visual studio

The API will be accessible at https://localhost:5191 by default. with swagger page

## 🧪 Testing
To run the tests:

```bash
cd ./base-reservation/api/BaseReservation.Tests
dotnet test
```
This will execute all unit and integration tests in the BaseReservation.Tests project.