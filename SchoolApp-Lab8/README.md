# School App Projects

Lab 8 of **Software Engineering** (FIT - TDTU): a School Management application built with the
**Code First** approach using **Entity Framework Core** and **SQL Server**, implemented twice -
once with ASP.NET Core Razor Pages and once with ASP.NET Core MVC.

## Repository layout

```
SchoolApp-Lab8/
|-- Exercise1_RazorPages/
|   |-- SchoolAppCoreRazor.sln
|   `-- SchoolAppCoreRazor/        # ASP.NET Core Razor Pages project
|-- Exercise2_MVC/
|   |-- SchoolAppCoreMVC.sln
|   `-- SchoolAppCoreMVC/          # ASP.NET Core MVC project
|-- Database/
|   |-- SchoolDB.sql               # schema + sample data for Exercise 1
|   `-- SchoolDBMVC.sql            # schema + sample data for Exercise 2
|-- Report/                        # Lab report (.docx)
`-- README.md
```

## Exercise 1: Razor Pages Application

- **Description:** ASP.NET Core Razor Pages project using Code First approach.
- **Instructions:** Open `Exercise1_RazorPages/SchoolAppCoreRazor.sln` in Visual Studio, press F5 to run.

## Exercise 2: MVC Application

- **Description:** ASP.NET Core MVC project using Code First approach.
- **Instructions:** Open `Exercise2_MVC/SchoolAppCoreMVC.sln` in Visual Studio, press F5 to run.

## Data model

| Entity | Key | Main columns | Relationships |
|---|---|---|---|
| `Department` | `DepartmentID` | `DepartmentName`, `Budget`, `StartDate` | 1 - N `Course` |
| `Course` | `CourseID` | `Title`, `Credits`, `DepartmentID` | N - 1 `Department`, 1 - N `Enrollment` |
| `Instructor` | `InstructorID` | `LastName`, `FirstName`, `HireDate` | none |
| `Student` | `StudentID` | `LastName`, `FirstName`, `EnrollmentDate` | 1 - N `Enrollment` |
| `Enrollment` | `EnrollmentID` | `CourseID`, `StudentID`, `Grade` | N - 1 `Course`, N - 1 `Student` |

`Student` and `Course` have a many-to-many relationship, resolved through `Enrollment`.

## Requirements

- Visual Studio 2022 (ASP.NET and web development workload)
- .NET 8 SDK
- SQL Server Express (instance name `SQLEXPRESS`) or change the connection string

## Create the database (migrate to MSSQL)

1. Check the connection string in `appsettings.json`
   (`SchoolDBConnectionString`, default `Server=(local)\SQLEXPRESS;Database=SchoolDB;...`).
2. **Build** the project first.
3. Open *Tools > NuGet Package Manager > Package Manager Console*, select the project as the
   *Default project*, then run:

```powershell
Add-Migration InitialCreate
Update-Database
```

EF Core creates the database and the tables `Departments`, `Instructors`, `Students`, `Courses`,
`Enrollments` and `__EFMigrationsHistory`.

> Alternative: run `Database/SchoolDB.sql` (or `SchoolDBMVC.sql`) in SQL Server Management Studio.
> Use either the migration **or** the script on a given database, not both.

## Notes

- The Course form shows departments as `ID - Name` in the dropdown. The Enrollment form does the same
  for courses and students.
- Bootstrap and jQuery are loaded from CDNs, so an internet connection is needed for styling.
