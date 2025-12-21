# Customer Management

ASP.NET Web Forms application for managing customers, targeting .NET Framework 4.8.1.

## Projects

- `CustomerManagement.WebApp` – Web Forms UI application
- `CustomerManagement.Business` – business logic (`CustomerService`, `CacheHelper`)
- `CustomerManagement.Data` – data access (`CustomerRepository`, `ICustomerRepository`)
- `CustomerManagement.Models` – domain models (`Customer`)
- `CustomerManagement.Tests` – unit tests for Business and Data layers

## Architecture

The application follows a layered architecture:
- Web layer handles UI and user interaction
- Business layer contains application logic and caching
- Data layer handles database access via ADO.NET
- Models define domain entities

## Features

- Customer list (`Customers.aspx`)
  - Search by name or email
  - Filter by status (Active / Inactive / All)
  - Add, edit, and delete customers
  - SQL paging and async data access
  - Results cached per search + status for 5 minutes
- Add customer (`CustomerAdd.aspx`)
- Edit customer (`CustomerForm.aspx`)
- Centralized error handling (`Global.asax` + `Error.aspx`)
- Cache invalidation via `CacheHelper.ClearCustomersCache()` on add/delete
- Fallback demo data in `CustomerService` when the database is not reachable

## Technologies

- ASP.NET Web Forms (.NET Framework 4.8.1)
- C#
- SQL Server LocalDB (`(localdb)\MSSQLLocalDB`)
- ADO.NET (async)
- MSTest (unit tests)

## Database

Connection string (`CustomerManagement.WebApp/Web.config`):

```xml
<connectionStrings>
  <add name="DefaultConnection"
       connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CustomerTest;Integrated Security=True"
       providerName="System.Data.SqlClient" />
</connectionStrings>
