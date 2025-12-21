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


## Business Logic & Validation

- Centralized customer operations in `CustomerService` (list, filter, get by id, add, update, delete)
- Search by first name, last name, or email (case-insensitive)
- Optional filtering by status (`IsActive`) via the status dropdown (`All / Active / Inactive`)
- Fallback to in-memory demo data when the database is not reachable (`GetDemoCustomers`)
- Duplicate email protection on create: `CustomerService.AddAsync` checks for existing email and throws an `InvalidOperationException` that is surfaced as a friendly error in the UI

## Validation

- Required fields and basic email regex on forms
- Duplicate email protection when adding a new customer (service throws and UI shows a friendly error message)


## Tests

1. Open Test Explorer in Visual Studio.
2. Select the `CustomerManagement.Tests` project.
3. Run all tests (`Run All`).

## TODO / Future Improvements

- Multi-select delete: allow selecting multiple customers in the grid and deleting them in a single action.
- Inline edit in the grid: enable editing certain fields (e.g., `IsActive`) directly in `Customers.aspx` without navigating to the edit form.
- Server-side paging & sorting: move paging/sorting from in-memory to database-side for better performance on large datasets.
- Authentication/authorization: restrict access to customer management pages (e.g., only for admin users).
- Export: add export of the filtered/sorted customer list to CSV/Excel.
- Improved error diagnostics in demo mode: optionally show a more descriptive message on `Error.aspx` in development (e.g., that the DB is not configured).
