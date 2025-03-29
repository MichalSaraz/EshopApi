# EshopApi

E-shop API is a modern ASP.NET Core Web API built with clean architecture principles. It provides RESTful endpoints for managing products and supports multiple API versions.

## Prerequisites
- .NET 8.0 SDK
- SQL Server (Docker container provided)
- Visual Studio or VS Code

## Project Structure
```bash
├── Domain/ 
│ ├── Entities/ 
│ └── Interfaces/ 
├── Application/ 
│ ├── Interfaces/ 
│ └── Services/ 
├── Infrastructure/ 
│ ├── Data/ 
│ │ └── Repositories/ 
│ └── DbContext/ 
├── Shared/ 
│ └── Dtos/ 
├── WebApi/ 
│ └── Controllers/ 
└── Tests/
```

## Architecture Overview
The application follows clean architecture principles, with each layer having specific responsibilities:

### Domain Layer
- Contains core business entities and interfaces
- Defines fundamental business rules
- Independent of external frameworks
- Includes domain-specific logic and validation

### Application Layer
- Implements business use cases
- Contains application services and interfaces
- Acts as intermediary between presentation and domain layers

### Infrastructure Layer
- Handles external concerns
- Implements data access patterns
- Manages database operations
- Contains framework-specific implementations

### Shared Layer
- Houses common types
- Defines data transfer objects
- Provides shared functionality across layers

### WebApi Layer
- Handles HTTP requests and responses
- Implements API endpoints
- Manages request validation
- Provides Swagger documentation

## Getting Started

1. **Clone the repository**
   ```sh
   git clone https://github.com/MichalSaraz/EshopApi.git
   ```

2. **Restore NuGet packages**
   ```sh
   dotnet restore
   ```

3. **Update connection string in appsettings.json**
   ```JSON
   {
     "ConnectionStrings": {
       "MasterConnection": "Server=localhost,1433;Database=EshopDB;User Id=sa;Password=yourPassword;TrustServerCertificate=true;"
     }
   }
   ```

4. **Apply database migrations**
   ```sh
   dotnet ef database update --project EshopApi.Infrastructure/EshopApi.Infrastructure.csproj
   ```

5. **Run the application**
   ```sh
   dotnet run --project EshopApi.WebApi/EshopApi.WebApi.csproj
   ```

## Running SQL Server in Docker

To run SQL Server in Docker:

   ```sh
   docker run -e 'ACCEPT_EULA=Y' \
              -e 'SA_PASSWORD=YourPassword123' \
              -p 1433:1433 \
              --name mssql-dev \
              -d mcr.microsoft.com/mssql/server:latest
   ```

Command explanation:

- ```-e 'ACCEPT_EULA=Y'``` - Accepts the license agreement
- ```-e 'SA_PASSWORD=YourPassword123'``` - Sets the system administrator password
- ```-p 1433:1433``` - Maps port 1433 from container to host
- ```--name mssql-dev``` - Names the container
- ```-d``` - Runs in detached mode (background)
- ```mcr.microsoft.com/mssql/server:latest``` - Uses the latest SQL Server image

## API Documentation
Swagger UI is available at ```/swagger``` when running locally. The API supports two versions:

- v1: ```/swagger/v1/swagger.json```
- v2: ```/swagger/v2/swagger.json```

## Running Tests
The project uses xUnit with Moq for unit testing. Required test packages:

- xUnit
- Moq
- Moq.EntityFrameworkCore
- Coverlet for code coverage

Run tests using:

   ```sh
   dotnet test EshopApi.Tests/EshopApi.Tests.csproj
   ```

## Security Considerations

### Connection String Security
- Never commit sensitive credentials to source control
- Use environment variables for sensitive data
- Keep connection strings in secure configuration files
- Use ```${VARIABLE_NAME}``` syntax for environment variables

### Docker Security
- Use strong passwords for SQL Server
- Keep container names and ports documented

### Contributing
- Fork the repository
- Create a feature branch
- Run tests: ```dotnet test```
- Submit a pull request with documentation updates

### Troubleshooting
If you encounter issues:
- Check the connection string in ```appsettings.json```
- Verify the SQL Server container is running
- Run ```dotnet restore``` if packages are missing
- Clear NuGet cache if needed: ```dotnet nuget locals all --clear```

This README provides a comprehensive guide to the project's architecture and setup while maintaining security best practices and clear documentation.
