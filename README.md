
# AVBlog

AVBlog is a clean architecture-based project designed to provide a modular and scalable blogging platform. It follows the principles of Clean Architecture to ensure separation of concerns, maintainability, and testability.

## Project Structure

The project is organized into the following layers:

1. **Domain Layer (`AVBlog.Domain`)**  
   - Contains core business logic and domain entities.
   - Independent of any other layers or external libraries.

2. **Application Layer (`AVBlog.Application`)**  
   - Implements application-specific logic, including commands, queries, and services.
   - Acts as a bridge between the domain and other layers.

3. **Infrastructure Layer (`AVBlog.Infrastructure`)**  
   - Handles external concerns such as database access and third-party integrations.
   - Implements repository interfaces defined in the domain layer.

4. **Presentation Layers**  
   - **WebAPI (`AVBlog.WebAPI`)**: Exposes RESTful APIs for the application.
   - **WebApp (`AVBlog.WebApp`)**: Provides a web-based user interface.

## Technologies Used

- **.NET 8.0**: The project targets the latest version of .NET for optimal performance and features.
- **CQRS**: Implements the Command Query Responsibility Segregation pattern to separate read and write operations.
- **Entity Framework Core**: Used for database access and management.
- **ASP.NET Core Identity**: Provides authentication and user management.
- **ASP.NET Core MVC**: Used for building the web-based user interface.
- **ASP.NET Core Web API**: Used for building RESTful APIs.
- **Scrutor**: Enables automatic dependency injection registration through assembly scanning.
- **MediatR**: Facilitates in-process messaging and decouples request/response handling.


## Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (or any other database supported by EF Core)

### Setup

1. Clone the repository
2. Update SQL ConnectionStrings in appsettings.json 
- AVBlogCommandContext (read-write)
- AVBlogQueryContext (read-only)

3. Set AVBlog.WebApp as startup project
4. Run application

NOTE: The migration run automatically and an account Admin will be created.

## License

This project is licensed under the [MIT License](LICENSE).