
# CarManagementAPI

## Overview

CarManagementAPI is a web API designed to manage car-related data efficiently. This project is built with ASP.NET Core and follows a structured architecture for easy scalability and maintenance. The primary functionalities include managing entities like cars, users, and associated operations via a RESTful API.

## Project Structure

The project structure is organized as follows:

- **Controllers**: Contains API controllers responsible for handling HTTP requests and returning appropriate responses.
- **Data**: Holds the database context and configurations for interacting with the database.
- **Dtos**: Data Transfer Objects used to encapsulate the data for requests and responses.
- **Entities**: Contains the core entity classes representing the database schema.
- **Filters**: Custom filters for handling cross-cutting concerns like exception handling and validation.
- **Migrations**: Stores migration files for managing database schema changes.
- **appsettings.json**: Configuration file for application settings, including the database connection string.
- **Program.cs**: Entry point for the application where services and middleware are configured.

## Database Setup

The application uses Microsoft SQL Server as its database. The connection string for the database is specified in the `appsettings.json` file:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=localhost,1433;Initial Catalog=DB_Car_Managment;User ID=sa;Password=parolaParola*;TrustServerCertificate=True"
}
```

### Connection String Explanation:

- **Data Source**: Specifies the database server (in this case, `localhost` at port `1433`).
- **Initial Catalog**: Defines the name of the database (`DB_Car_Managment`).
- **User ID**: The username for authentication (`sa`).
- **Password**: The password for the database user (`parolaParola*`).
- **TrustServerCertificate**: Ensures the server certificate is trusted.

### Setting Up the Database

1. Ensure that Microsoft SQL Server is installed and running.
2. Update the connection string in the `appsettings.json` file with your database credentials, if different.
3. Run the migrations to set up the database schema:
   ```bash
   dotnet ef database update
   ```

## How to Run the Application

1. Clone the repository:
   ```bash
   git clone <repository-url>
   ```
2. Navigate to the project directory:
   ```bash
   cd CarManagementApi
   ```
3. Restore the dependencies:
   ```bash
   dotnet restore
   ```
4. Run the application:
   ```bash
   dotnet run
   ```
5. Access the API endpoints via a tool like Postman or a web browser at `http://localhost:<port>`.

## Key Features

- **Entity Management**: Perform CRUD operations on car-related entities.
- **DTO Validation**: Ensures data integrity and validation before processing requests.
- **Structured Architecture**: Follows best practices for separation of concerns and scalability.

## Dependencies

The project uses the following dependencies:

- **Entity Framework Core**: For database interactions.
- **ASP.NET Core**: For building the web API.
- **FluentValidation** (optional): For request validation.

## Contribution

1. Fork the repository.
2. Create a new branch:
   ```bash
   git checkout -b feature-branch
   ```
3. Make your changes and commit them:
   ```bash
   git commit -m "Add feature"
   ```
4. Push to the branch:
   ```bash
   git push origin feature-branch
   ```
5. Open a pull request.

## License

This project is licensed under the MIT License. See the LICENSE file for details.