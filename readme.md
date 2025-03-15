# EmployeeMaintenance API Documentation

1. **[Overview](#overview)**
2. **[How to Run](#how-to-run)**
3. **[Technologies Used](#technologies-used)**
4. **[Key Features](#key-features)**
5. **[Architecture](#architecture)**
6. **[Endpoints](#endpoints)**
7. **[Testing](#testing-1)**
8. **[Database Structure](#database-structure)**

--- 

## Overview

This document provides an overview of the **EmployeeMaintenance API** architecture and its main components. The API is structured using a **clean architecture approach**, with clear separation of concerns across different layers. The main layers include:

- **API**: Handles HTTP requests and responses, serving as the entry point for the API.
- **Application**: Contains the application logic, including **CQRS patterns** and **MediatR** for handling commands and queries.
- **Infra**: Manages infrastructure concerns such as database access, external services, and other cross-cutting concerns.
- **Domain**: Encapsulates the core business logic and domain models.
- **Tests**: Contains unit and integration tests to ensure the reliability and correctness of the application.

---

## How to Run

To set up and run the **EmployeeMaintenance API**, follow the steps below. Ensure you have **Docker** installed as a prerequisite, as the application is containerized for easy deployment.

### Prerequisites
- **Docker**: You need to have Docker installed on your system. If you don't have it installed, download and install Docker from the official site: [Docker](https://www.docker.com/get-started).

### Steps to Run

1. **Clone the repository**:
   ```bash
   git clone https://github.com/TiagoEsdras/.net-fullstack-assessment.git
   ```

2. **Navigate to the project folder**:
   ```bash
   cd SimpleEmployeeMaintenance
   ```

3. **Build and run the application** using Docker:
   ```bash
   docker-compose up --build
   ```

4. **Wait for the services to start**. Docker will create containers for the SQL Server database, the API and FrontEnd.

### Running the Services

Once the Docker containers are up and running, you can access the following services:

- **API**: The EmployeeMaintenance API will be accessible at [http://localhost:8081](http://localhost:8081).
- **Frontend**: The frontend application will be accessible at [http://localhost:3000](http://localhost:3000).
- **Database**: The server database will be accessible at localhost,1433
  
Both services should be up and running, allowing you to interact with the API via the frontend or tools like Postman.

---

## Technologies Used

The application leverages the following technologies and libraries to ensure functionality, maintainability, and scalability:

### Core Platform
- **.NET 8**: The latest Long-Term Support (LTS) release from Microsoft, providing a modern and high-performance platform for building scalable applications. Key features include:
  - Improved runtime performance and garbage collection.
  - Native AOT (Ahead-of-Time) compilation for reduced startup time and memory usage.
  - Cross-platform support for Windows, Linux, and macOS.

### Libraries
- **FluentValidation** (v11.11.0): Used for validating request models and enforcing business rules with a fluent and expressive API.
- **AutoMapper** (v14.0.0): Simplifies object-to-object mapping, reducing boilerplate code when transforming data between layers.
- **MediatR** (v12.4.1): Implements the mediator pattern, enabling **CQRS (Command Query Responsibility Segregation)** for handling commands and queries in a decoupled manner.
- **Swashbuckle.AspNetCore** (v6.6.2): Automatically generates **Swagger documentation** for the API, making it easier to explore and test endpoints.

### Database and ORM
- **Microsoft.EntityFrameworkCore** (v8.0.13): The core library for Entity Framework, providing ORM (Object-Relational Mapping) capabilities.
- **Microsoft.EntityFrameworkCore.SqlServer** (v8.0.13): Enables integration with SQL Server databases using Entity Framework Core.
- **Microsoft.EntityFrameworkCore.Design** (v8.0.13): Supports design-time tools for Entity Framework, such as migrations and scaffolding.
- **Microsoft.EntityFrameworkCore.Tools** (v8.0.13): Provides CLI tools for managing database migrations and other EF-related tasks.

### Testing
- **xUnit** (v2.9.3): A testing framework for writing and executing unit and integration tests.
- **FluentAssertions** (v8.1.1): Provides a fluent API for writing assertions, making tests more readable and expressive.
- **Moq** (v4.20.72): A mocking library for creating mock objects and simulating dependencies in unit tests.
- **Bogus** (v35.6.2): A library for generating realistic fake data to simulate test cases.

### Containerization
- **Docker**: Used to containerize the application, ensuring consistent environments across development, testing, and production. The `Dockerfile` is configured to build and run the .NET 8 application and SQL Server, making it easy to deploy and scale in containerized environments.

---

## Key Features

The API provides the following key features:

### Department Management
- List all departments with pagination support.

### Employee Management
- Create a new employee.
- List all employees with pagination support.
- Retrieve an employee by their unique identifier.
- Soft delete an employee.
- Update an employee's department.

---

## Architecture

### CQRS and MediatR
The application layer utilizes the **Command Query Responsibility Segregation (CQRS)** pattern, with **MediatR** acting as the mediator for handling commands and queries. This approach ensures a clear separation between read and write operations, improving scalability and maintainability.

### Service Layer
The `EmployeeService` class orchestrates complex operations related to employee management, such as creating an employee and updating an employee's department. This service layer abstracts the business logic from the controllers, promoting reusability and testability.

### Controllers
The API exposes two main controllers:

1. **DepartmentController**: Handles department-related operations.
2. **EmployeeController**: Manages employee-related operations, including creation, retrieval, deletion, and updates.

---

## Endpoints

The API endpoints are designed to be RESTful, with clear and consistent naming conventions. Each endpoint is mapped to a specific HTTP method and route, ensuring intuitive usage.

### Department Endpoints
- `GET /api/department`: List all departments with pagination.

### Employee Endpoints
- `POST /api/employee`: Create a new employee.
- `GET /api/employee`: List all employees with pagination.
- `GET /api/employee/{id}`: Retrieve an employee by ID.
- `DELETE /api/employee/{id}`: Soft delete an employee.
- `PUT /api/employee/{id}`: Update an employee's department.

---

## Testing

The **Tests** layer ensures the reliability and correctness of the application. The following components have been thoroughly tested:

### Test Coverage
- **Handlers**: All MediatR handlers for commands and queries have been tested to ensure they behave as expected.
- **Services**: The `EmployeeService` class, which orchestrates employee-related operations, has been tested to validate its logic and ensure proper integration with other components.
- **Validators**: Validators for request models (e.g., `EmployeeRequestDto`, `DepartmentRequestDto`) have been tested to ensure they enforce business rules and validation constraints.
- **Other Classes**: Any additional classes that require validation or complex logic have also been tested to ensure they function correctly.

This comprehensive testing approach ensures the reliability and maintainability of the application.

---

## Database Structure

The database schema is designed to support the core functionalities of the application. Below is the Entity-Relationship Diagram (ERD) that represents the structure of the database:

![Database ERD](https://i.imgur.com/w9nCDrK.jpeg)
