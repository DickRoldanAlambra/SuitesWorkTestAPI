SuiteWorks Test API

Overview

This repository contains the SuiteWorks Test API. This README covers local setup, running the API, and database migration steps (EF Core). It also includes notes for migrating projects to .NET 10.

Prerequisites

- .NET 10 SDK installed (https://dotnet.microsoft.com)
- Visual Studio 2026 or VS Code (optional)
- (For EF Core migrations) dotnet-ef tool: dotnet tool install --global dotnet-ef
- A database server (SQL Server, PostgreSQL, etc.) and a connection string

Quick start (local)

1. Clone the repo
   git clone https://github.com/DickRoldanAlambra/SuitesWorkTestAPI.git
   cd SuitesWorkTestAPI

2. Restore and build
   dotnet restore
   dotnet build --no-restore

3. Configure settings
   - Edit appsettings.Development.json or appsettings.json in the API project and set the connection string(s).
   - Alternatively set environment variables (example PowerShell):
     $env:ConnectionStrings__DefaultConnection = "Server=.;Database=SuiteWorksDb;Trusted_Connection=True;"

4. Run the API
   dotnet run --project <Path/To/ApiProject.csproj>
   - Or open SuiteWorksTestAPI.slnx in Visual Studio and press F5.

Entity Framework Core migrations (database)

1. Install EF Core CLI (if not installed):
   dotnet tool install --global dotnet-ef

2. Create a migration (replace placeholders):
   dotnet ef migrations add InitialCreate --project <Path/To/ApiProject.csproj> --startup-project <Path/To/ApiProject.csproj>

3. Apply migrations to the database:
   dotnet ef database update --project <Path/To/ApiProject.csproj> --startup-project <Path/To/ApiProject.csproj>

Notes:
- If your solution separates the data layer and the startup project, use --project to point to the project that contains the DbContext and --startup-project to point to the project that contains the Program/Startup class.
- If migrations fail with type resolution errors, ensure the startup project builds and has references to the same DbContext assembly.

Migrating this project to .NET 10

1. Update your project files (.csproj)
   - Change the TargetFramework element to net10.0:
     <TargetFramework>net10.0</TargetFramework>
   - If the project uses older package references, consider migrating to SDK-style references and implicit usings where appropriate.

2. Update the SDK version (optional)
   - If you use global.json to pin SDK: update to an SDK that supports .NET 10.

3. Update NuGet packages
   - Check outdated packages: dotnet list package --outdated
   - Update packages carefully, prefer one package at a time and run tests/build after each change.

4. Build and test
   dotnet restore
   dotnet build
   dotnet test (if tests exist)

5. Verify runtime behaviors
   - Run the app and test endpoints.
   - Review Microsoft breaking changes for the .NET versions you are migrating from.

Troubleshooting

- "dotnet ef" cannot be found: ensure dotnet-ef is installed globally or reference Microsoft.EntityFrameworkCore.Design in the project.
- Migration errors: confirm correct DbContext type is discovered (Program/Startup must properly register the DbContext for the environment used by dotnet-ef).

Links

- .NET downloads and docs: https://dotnet.microsoft.com
- EF Core docs (migrations): https://learn.microsoft.com/ef/core/managing-schemas/migrations/
- Migrating to .NET: https://learn.microsoft.com/dotnet/core/porting/

If you want, tell me the API project path (the folder containing the main API .csproj). I can add concrete dotnet-ef commands and a tailored setup section for this repository.
