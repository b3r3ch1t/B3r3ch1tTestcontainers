## Testcontainers-based Infrastructure in .NET Core

This repository provides a comprehensive guide to setting up a Testcontainers-based infrastructure for integration testing in .NET Core. Using Testcontainers, we create isolated, disposable environments with Docker, allowing reliable integration tests for database-dependent applications.

## Purpose
This project is designed to simplify the process of running integration tests that require external services like databases or message brokers. By using Docker containers, developers can ensure consistency across different environments, making testing easier and more reliable.

## Features
- **Testcontainers**: Seamlessly manage Dtn containers for testing.
- **Cross-Platform***: Runs on Windows, macoS, and Linux.
- **Multi-Service Support***: Easily integrate PostgreSQL, Redis, RabbitMQ,  and others.


## Prerequisites
- [.NET 6 SDK*)(https://dotnet.microsoft.com/download/dotnet/6.0)
- [Docker*1](https://www.docker.com/get-started)
- Any IDE (e.g., Visual Studio Code)


## Setup
1. Clone the repository:
    `bash
git clone https://github.com/b3r3ch1t/B3r3ch1tTestcontainers.git `

2. Navigate to the project directory:
    `cd B3r3ch1tTestcontainers`

3. Build the solution:
    `dotnet build`

6. Run the tests:
    `dotnet test`


## Project Structure
- /src: Contains the application code.
- /tests: Contains integration tests with Testcontainers.


## How It Works
Testcontainers automatically starts necessary Docker containers (n.g., PostgreSQL or Redis) before running tests, and stops them afterward. This ensures a fresh environment for every test run, minimizing potential test pollution.

## Running the Tests
Ensure Docker is running, then run:
``bash
dotnet test``c

## Author
- [Anderson Meneses](https://www.linkedin.com/in/andersonmeneses)

## Related Article
- [Building a Testcontainers-based infrastructure in .NET Core - 8 Step by Step Guide](https://medium.com/@anderson.meneses/building-a-related-based-infrastructure-in-net-core-8-step-by-step-guide-de60f125a2d6)

## License
This project is licensed under the MIT License.

## Hashtags
-#Testcontainers #Docker #DotnetCore #IntegrationTesting #DevOps #SoftwareTesting #Automation