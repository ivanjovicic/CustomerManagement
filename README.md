# Customer Management

ASP.NET Web Forms application for managing customers, targeting .NET Framework 4.8.1.

## Projects

- `CustomerManagement.WebApp` ? Web Forms UI application
- `CustomerManagement.Business` ? business logic (`CustomerService`, `CacheHelper`)
- `CustomerManagement.Data` ? data access (`CustomerRepository`, `ICustomerRepository`)
- `CustomerManagement.Models` ? domain models (`Customer`)
- `CustomerManagement.Tests` ? unit tests for Business and Data layers

## Features

- Customer list (`Customers.aspx`)
  - Search by name/email
  - Filter by status (Active / Inactive / All)
  - Add, edit, delete customers
  - Results cached per search + status for 5 minutes
  - New customers appear on the last page because data is ordered by `Id` ascending
- Add customer (`CustomerAdd.aspx`)
- Edit customer (`CustomerForm.aspx`)
- Centralized error handling (`Global.asax` + `Error.aspx`)
- Cache invalidation via `CacheHelper.ClearCustomersCache()` on add/delete
- Fallback demo data in `CustomerService` when the database is not reachable (UI stays functional even without DB)
- When the database is not available, the app runs in read-only demo mode:
  - `DatabaseInitializer` sets `AppState.IsDatabaseAvailable = false` on DB init failure
  - `Customers.aspx` shows a warning banner and disables/hides Add/Delete actions
  - `CustomerForm.aspx` loads customer data with `GetByIdAsync` fallback to demo data and disables the Save button
  - Filtering by status and search by name/email do not update any persisted data (demo data is in-memory only)

## Technologies

- ASP.NET Web Forms (.NET Framework 4.8.1)
- C#
- SQL Server LocalDB (`(localdb)\\MSSQLLocalDB`)
- MSTest (unit tests)

## Database

Connection string (`CustomerManagement.WebApp/Web.config`):

```xml
<connectionStrings>
  <add name="DefaultConnection"
       connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CustomerTest;Integrated Security=True"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

Expected database: `CustomerTest` with table `dbo.Customers`.

### Database initialization

On application start (`Global.asax`), the app calls `DatabaseInitializer.RunInitScript()` which:
- Checks if the `CustomerTest` database already exists via `DB_ID('CustomerTest')`.
- If it does not exist, executes `App_Data/Sql/CreateDatabaseAndCustomersTable.sql`.
- That script creates the database, the `dbo.Customers` table and seeds around 20 random demo customers (only if the table is empty).

You can also run `App_Data/Sql/CreateDatabaseAndCustomersTable.sql` manually in SQL Server Management Studio against your `(localdb)\MSSQLLocalDB` instance.

## How to Run

1. Open the solution `CustomerManagementSystem.sln` in Visual Studio.
2. Set `CustomerManagement.WebApp` as the startup project.
3. Ensure LocalDB instance `(localdb)\\MSSQLLocalDB` is running and the `CustomerTest` database exists.
4. Press `F5` to run.
5. The application will open on `Customers.aspx` (configured as default document in `Web.config`).

## Tests

1. Open Test Explorer in Visual Studio.
2. Select the `CustomerManagement.Tests` project.
3. Run all tests (`Run All`).