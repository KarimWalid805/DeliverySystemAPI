# Delivery System API

ASP.NET Core Web API for the online ordering and delivery system used by the [EFTrial WinForms client](https://github.com/KarimWalid805/EFTrial). It provides account registration and sign-in, product and category management, customers, orders, drivers, and deliveries. Data is stored with Entity Framework Core and SQL Server.

## Features

- Customer and driver registration and customer, driver, and admin sign-in.
- CRUD endpoints for products and categories.
- Customer, driver, order, and delivery endpoints used by the desktop client.
- Entity Framework Core models, relationships, and seeded sample categories, products, and drivers.
- Dockerfile for container builds.

## Technology

- ASP.NET Core Web API on .NET 9
- Entity Framework Core 9
- SQL Server via `Microsoft.EntityFrameworkCore.SqlServer`
- Docker

## Requirements

- .NET 9 SDK, or Docker
- A SQL Server database whose schema matches `onlineDeliveryDbContext`
- Database connection settings (see below)

## Run locally

1. Clone the repository and enter the project directory:

   ```sh
   git clone https://github.com/KarimWalid805/DeliverySystemAPI.git
   cd DeliverySystemAPI
   ```

2. Configure the SQL Server connection using environment variables or the `ConnectionStrings:DefaultConnection` setting in `appsettings.json`.

3. Restore dependencies and run the API:

   ```sh
   dotnet restore DeliverySystemAPI.csproj
   dotnet run --project DeliverySystemAPI.csproj
   ```

   The included launch profile uses `http://localhost:5041` and `https://localhost:7294`. The HTTPS profile may require a trusted local ASP.NET Core development certificate.

The application does not include EF Core migrations or a database creation script. Provision a database with tables matching the entity models before using the endpoints. Sample values configured with `HasData` are applied only as part of a database migration/update process.

## Database configuration

`Program.cs` builds the SQL Server connection string from these environment variables when all four are set:

| Variable | Value |
| --- | --- |
| `DB_SERVER` | SQL Server host and, if needed, port |
| `DB_NAME` | Database name |
| `DB_USER` | Database username |
| `DB_PASSWORD` | Database password |

If any variable is missing, the application falls back to `ConnectionStrings:DefaultConnection` in `appsettings.json`. Keep real credentials out of source control and configure them through the deployment environment.

## API endpoints

Routes use the `api/[controller]` convention. The main resource paths are shown in lowercase; ASP.NET Core routing is case-insensitive.

| Method | Route | Purpose |
| --- | --- | --- |
| `POST` | `/api/auth/customer` | Sign in a customer |
| `POST` | `/api/auth/driver` | Sign in a driver |
| `POST` | `/api/auth/admin` | Sign in an admin |
| `POST` | `/api/register/customer` | Register a customer account |
| `POST` | `/api/register/driver` | Register a driver account |
| `GET`, `POST` | `/api/customer` | List or create customers |
| `GET`, `POST` | `/api/drivers` | List or create drivers |
| `GET`, `POST` | `/api/product` | List or create products |
| `GET`, `PUT`, `DELETE` | `/api/product/{id}` | Get, update, or delete a product |
| `DELETE` | `/api/product/category/{categoryId}` | Delete all products in a category |
| `GET`, `POST` | `/api/category` | List or create categories |
| `GET`, `PUT`, `DELETE` | `/api/category/{id}` | Get, update, or delete a category |
| `GET` | `/api/order` | List orders with customer details |
| `GET` | `/api/order/{id}` | Get one order with customer details |
| `POST` | `/api/order/customerForm` | Create an order from the customer workflow |
| `POST` | `/api/order/driverForm` | Create an order from the driver workflow |
| `GET`, `POST` | `/api/delivery` | List or create deliveries |
| `GET` | `/api/delivery/{id}` | Get one delivery |

Deleting a category removes its related products.

The API returns JSON. The EFTrial desktop app calls these routes for login/registration, catalog operations, customers, orders, drivers, and deliveries. Its API base URLs are currently embedded in the WinForms source.

## Design and implementation

- **Controller-based API:** resource controllers define HTTP routes and translate requests into application operations.
- **Dependency injection:** controllers receive `onlineDeliveryDbContext` through constructor injection, registered in `Program.cs`.
- **Entity Framework Core / Unit of Work:** controllers query and update entities through the injected `DbContext`, then persist changes with `SaveChangesAsync()`.
- **Entity relationships:** the context configures customer-to-orders, order-to-delivery, driver-to-delivery, and category-to-products relationships.
- **DTO projection:** order list and detail endpoints project related entities into `OrderDTO` and `CustomerDTO` responses.
- **Asynchronous I/O:** database queries and writes use EF Core async methods.

## Security and scope notes

- Account passwords are currently stored and compared as plain text in the account endpoints. This implementation is suitable only as a demo; use password hashing and stronger authentication before handling real accounts.
- The API does not configure an authentication scheme or authorization policies. `UseAuthorization()` alone does not protect the resource endpoints.
- `Controllers/AuthController.cs` authenticates against account records and returns account details; the API does not issue tokens or establish an authenticated session.
- The repository contains the API project and models, but not a database setup script or the EFTrial WinForms client.

## License

No license file is included. Contact the repository owner for reuse and distribution terms.
