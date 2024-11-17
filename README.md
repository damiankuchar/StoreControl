# ASP.NET Core Web API Template

This repository provides a robust and modular ASP.NET Core Web API template designed to streamline the setup and development of new projects. It eliminates the repetitive setup process and accelerates the project initiation phase. The template is built on modern development principles and technologies, ensuring scalability, maintainability, and clean architecture.

---

## ✨ Features

### Frameworks and Tools
- **ASP.NET Core 8.0 (Web API)**
- **Entity Framework Core (EFCore)**
- **PostgreSQL**
- **Seq**
- **PgAdmin**

Popular services not included:

- **Grafana and Prometheus** to collect metrics (not implemented)
- **Azurite** - Microsoft Azure Blob Storage, Queue Storage and Table Storage for local development (not implemented)
- **Redis** (not implemented)

### Containerization and Environments
- **Docker and Docker Compose:** Simplifies local development and deployment.
- **Environment Configurations:** Predefined environments for Local, DEV, and more.

### Architecture and Design Patterns
- **Clean Architecture:** Promotes separation of concerns and maintainable code.
- **CQRS (Command and Query Responsibility Segregation):** Separates read and write responsibilities for cleaner design.
- **MediatR:** Simplifies in-process messaging and CQRS implementation.

### Security
- **Custom Permission-Based Authentication:** Fine-grained control over resource access.

<br>

***Notice that I do not use Repository/UnitOfWork patterns.***

---

## 📦 Project Structure

### src/
- **StoreControl.Application/**  
  Application layer: CQRS, MediatR handlers, validations.
  
- **StoreControl.Domain/**  
  Core business logic: entities, enums, value objects.
  
- **StoreControl.Infrastructure/**  
  Infrastructure layer: EFCore, PostgreSQL, external services.
  
- **StoreControl.WebAPI/**  
  Web API project: controllers, middlewares, endpoints.

### tests/
- **StoreControl.Application.IntegrationTests/**  
  Integration tests for API endpoints and database interactions.

---

## 🧪 Testing

This project uses **xUnit** as the testing framework due to its simplicity, flexibility, and seamless integration with .NET tooling. Testing is an essential part of the development process, ensuring that the application is robust and behaves as expected.

### Current Test Setup
- **Integration Tests:**  
  Currently, the project includes integration tests, located in the `tests/StoreControl.Application.IntegrationTests` folder. These tests validate the interaction between various components, such as API endpoints and database operations.

### Extensibility
While the current focus is on integration tests, the testing setup is designed to be easily extensible. You can add other types of tests, such as:
- **Unit Tests:** For testing individual components or business logic in isolation.
- **End-to-End (E2E) Tests:** For testing the entire workflow of the application.

### Running Tests
Run all tests using the .NET CLI:
```bash
dotnet test
```

---

## 🚀 Getting Started

### Prerequisites
1. **.NET SDK 8.0**
2. **Docker**

The recommended development approach is to use Docker containers (`docker-compose up`). This will set up all the necessary dependencies and external services like the database, Seq, etc., allowing you to mock the entire environment locally.

For running the application, I recommend using `dotnet run` or launching it from Visual Studio for the fastest and most seamless experience. While it’s possible to run everything via a single Docker Compose file (which also supports the debugger), this approach is slower.
