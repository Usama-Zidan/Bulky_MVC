# Bulky Book Store

An educational online bookstore built with ASP.NET Core MVC, Entity Framework Core, SQL Server, and ASP.NET Core Identity. The project demonstrates a layered .NET application with repository abstractions, Unit of Work, MVC Areas, role-based authorization, database migrations, and product management.

> This is a learning project rather than a production-ready commerce platform. Payment processing, checkout, inventory, and order management are intentionally outside the current scope.

## Features

- Customer storefront with a responsive Bootstrap layout
- Book catalog seeded with sample titles, authors, prices, ISBNs, categories, and cover images
- Product details page
- Admin area protected by the `Admin` role
- Create, edit, and delete operations for categories
- Create, update, delete, and image upload operations for products
- ASP.NET Core Identity pages for registration, login, logout, account management, password reset, and two-factor authentication
- SQL Server persistence through Entity Framework Core migrations
- Repository and Unit of Work abstractions separating the web layer from data access
- Nullable reference types and implicit usings enabled across the .NET projects

## Screenshots

### Customer Catalog
> To be added.

### Product Details
> To be added.

### Category Details
> To be added.

## Technology stack

- **.NET 8**
- **ASP.NET Core MVC**
- **Razor Pages** for Identity UI
- **Entity Framework Core 8**
- **SQL Server**
- **ASP.NET Core Identity**
- **Bootstrap**, **jQuery**, and unobtrusive validation
- **Visual Studio 2022** or the .NET CLI

## Solution structure

```text
Bulky_MVC/
├── Bulky.sln
├── BulkyWeb/                  # Main ASP.NET Core MVC application
│   ├── Areas/
│   │   ├── Customer/          # Storefront and product details
│   │   ├── Admin/             # Admin-only category and product management
│   │   └── Identity/          # Authentication and account pages
│   └── wwwroot/               # CSS, JavaScript, libraries, and product images
├── Bulky.DataAccess/          # DbContext, migrations, repositories, Unit of Work
├── Bulky.Models/              # Entities, view models, and ApplicationUser
├── Bulky.Utility/             # Shared constants and utility services
└── BulkyWeb_Razor/            # Separate Razor Pages learning project
```

The solution currently builds the `BulkyWeb`, `Bulky.DataAccess`, `Bulky.Models`, and `Bulky.Utility` projects. `BulkyWeb_Razor` is retained as a separate Razor Pages learning implementation and is not included in `Bulky.sln`.

## Prerequisites

Install the following before running the application:

1. [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
2. [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) or SQL Server Express/LocalDB
3. Optional: [Visual Studio 2022](https://visualstudio.microsoft.com/) with the **ASP.NET and web development** workload

## Configuration

The main MVC application reads its database connection from:

```text
BulkyWeb/appsettings.json
```

Before running the project, replace the sample `DefaultConnection` value with a connection string for your SQL Server instance. Do not commit passwords or other secrets to `appsettings.json`; use user secrets or environment variables for sensitive configuration.

Example SQL Server connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=Bulky;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

## Getting started

From the repository root:

```bash
dotnet restore Bulky.sln
dotnet build Bulky.sln
```

Apply the existing Entity Framework Core migrations:

```bash
dotnet ef database update --project Bulky.DataAccess --startup-project BulkyWeb
```

If the `dotnet ef` command is not installed, install the EF Core CLI tool once:

```bash
dotnet tool install --global dotnet-ef
```

Run the MVC application:

```bash
dotnet run --project BulkyWeb
```

The HTTPS launch profile uses `https://localhost:7235` by default. The HTTP profile uses `http://localhost:5242`. The exact URL is printed in the terminal when the application starts.

To run it from Visual Studio, open `Bulky.sln`, set **BulkyWeb** as the startup project, update the connection string, apply the migrations, and press **Ctrl+F5** or **F5**.

## Using the application

### Customer experience

- Open the home page to browse the seeded books.
- Select a book to view its details.
- Register or sign in through the Identity UI.

### Administration

The admin controllers are protected with the `Admin` role. After creating a user, assign the `Admin` role in the database or extend the application with a role-seeding workflow before opening:

```text
/Admin/Category
/Admin/Product
```

The admin product form supports category selection, pricing fields, and cover-image upload. Uploaded images are stored below `BulkyWeb/wwwroot/images/product`.

## Data model

The main database contains:

- `Categories`
- `Products`
- ASP.NET Core Identity tables
- `ApplicationUser` profile data, including name and address fields

The initial migration history and sample catalog data are stored in `Bulky.DataAccess/Migrations` and `ApplicationDbContext`.

## Learning goals

This project is useful for practicing:

- ASP.NET Core MVC routing and Areas
- Razor views and partial views
- Model validation and view models
- Dependency injection
- Generic repositories and Unit of Work
- EF Core relationships, migrations, and seed data
- File uploads and static files
- Authentication and role-based authorization
- Organizing a multi-project .NET solution

## Notes

- The connection strings in the repository are development examples and should be customized locally.
- The sample catalog data is fictional and intended for demonstration.
- The application currently focuses on catalog browsing and administration; shopping cart and order workflows are not implemented in the current solution.

## License

This repository is an educational project. No license has been specified. 
