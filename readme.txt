# Implementing a .NET Solution with Clean Architecture, DDD, EF Core, Hangfire, and Vue.js

## 1. Creating the .NET Solution

We start by setting up a .NET solution using the latest .NET version, implementing it in C#. The solution will contain multiple projects to separate concerns and ensure a maintainable architecture.

```sh
mkdir MySolution && cd MySolution

# Create the solution file
dotnet new sln -n MySolution
```

## 2. Project Structure

The solution consists of two primary hosts, each in its own `.csproj` file:
- A **WebAPI** project to expose REST endpoints.
- A **WorkerService** project to run background jobs.

```sh
# Create Web API host
mkdir src/WebAPI && cd src/WebAPI

dotnet new webapi -n MySolution.WebAPI
cd ../..

dotnet sln add src/WebAPI/MySolution.WebAPI.csproj

# Create Worker Service host
mkdir src/WorkerService && cd src/WorkerService

dotnet new worker -n MySolution.WorkerService
cd ../..

dotnet sln add src/WorkerService/MySolution.WorkerService.csproj
```

## 3. Architecture (Clean Architecture & DDD)

The project follows **Clean Architecture** principles combined with **Domain-Driven Design (DDD)**. Each layer is implemented as a separate Class Library:

- **Domain Layer:** Contains business rules, domain entities, and aggregates.
- **Application Layer:** Contains use cases, services, and business logic.
- **Infrastructure Layer:** Handles data persistence, external APIs, and configurations.
- **Presentation Layer:** The WebAPI for exposing endpoints.

```sh
# Create domain layer
mkdir src/Core/Domain && cd src/Core/Domain

dotnet new classlib -n MySolution.Domain
cd ../../..

dotnet sln add src/Core/Domain/MySolution.Domain.csproj

# Create application layer
mkdir src/Core/Application && cd src/Core/Application

dotnet new classlib -n MySolution.Application
cd ../../..

dotnet sln add src/Core/Application/MySolution.Application.csproj

# Create infrastructure layer
mkdir src/Infrastructure && cd src/Infrastructure

dotnet new classlib -n MySolution.Infrastructure
cd ../..

dotnet sln add src/Infrastructure/MySolution.Infrastructure.csproj
```

Each component follows **SOLID principles** for maintainability and extensibility.

## 4. Data Storage with Repository Pattern & EF Core

We use **Entity Framework Core (EF Core)** for data persistence and implement the **Repository Pattern** to abstract data access.

```sh
# Install EF Core packages
cd src/Infrastructure

dotnet add package Microsoft.EntityFrameworkCore

dotnet add package Microsoft.EntityFrameworkCore.SqlServer

dotnet add package Microsoft.EntityFrameworkCore.Design
cd ../..
```

A `Repository` interface is defined in `MySolution.Application`, and its implementation is provided in `MySolution.Infrastructure`.

## 5. Data Upsert with Hangfire Jobs

We use **Hangfire** to schedule and execute jobs for data upsert. The job runs hourly and is hosted within the **WorkerService** project.

```sh
# Install Hangfire packages
cd src/WorkerService

dotnet add package Hangfire

dotnet add package Hangfire.SqlServer
cd ../..
```

Jobs are placed in a separate **Class Library** for better organization:

```sh
mkdir src/Core/Jobs && cd src/Core/Jobs

dotnet new classlib -n MySolution.Jobs
cd ../../..

dotnet sln add src/Core/Jobs/MySolution.Jobs.csproj
```

The WorkerService is configured to run the job every hour using Hangfire’s background job processing system.

## 6. Data Source

Data is fetched from a public API listed in the [Public API List](https://github.com/public-api-lists/public-api-lists). The API consumption logic is implemented in the **Infrastructure Layer**.

## 7. Database: MSSQL

We use **Microsoft SQL Server (MSSQL)** for data storage. The connection string is configured in `appsettings.json`, and migrations are managed via EF Core.

```sh
# Run EF Core migration
cd src/Infrastructure

dotnet ef migrations add InitialCreate --startup-project ../WebAPI/MySolution.WebAPI.csproj

dotnet ef database update --startup-project ../WebAPI/MySolution.WebAPI.csproj
cd ../..
```

## 8. Frontend: Vue.js with TypeScript

A Vue.js frontend is added to display data in a **filterable grid**. The frontend communicates with the WebAPI backend.

```sh
# Create Vue.js project
mkdir frontend && cd frontend

npm create vue@latest my-frontend
cd my-frontend

npm install
npm install axios
```

The frontend fetches data from the WebAPI and displays it in a simple grid. It uses **Axios** to make HTTP requests and **TypeScript** for type safety.

---

This setup provides a clean, scalable, and maintainable architecture with **.NET Clean Architecture, DDD, EF Core, Hangfire Jobs, and a Vue.js frontend**. 🚀

