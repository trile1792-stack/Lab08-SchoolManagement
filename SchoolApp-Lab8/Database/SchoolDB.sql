/* =====================================================================
   Lab 8 - School Management (ASP.NET Core Code First)
   Database : SchoolDB
   Server   : SQL Server / SQL Server Express
   Purpose  : Standalone script equivalent to the schema produced by
              Add-Migration InitialCreate + Update-Database.
   NOTE     : Run this script INSTEAD of Update-Database, not in addition
              to it (the script does not create __EFMigrationsHistory).
   ===================================================================== */

IF DB_ID(N'SchoolDB') IS NULL
    CREATE DATABASE [SchoolDB];
GO

USE [SchoolDB];
GO

DROP TABLE IF EXISTS dbo.Enrollments;
DROP TABLE IF EXISTS dbo.Courses;
DROP TABLE IF EXISTS dbo.Students;
DROP TABLE IF EXISTS dbo.Instructors;
DROP TABLE IF EXISTS dbo.Departments;
GO

/* ---------- Tables ---------- */
CREATE TABLE [dbo].[Departments] (
    [DepartmentID]   INT            IDENTITY(1,1) NOT NULL,
    [DepartmentName] NVARCHAR(MAX)  NOT NULL,
    [Budget]         DECIMAL(18,2)  NOT NULL,
    [StartDate]      DATETIME2(7)   NOT NULL,
    CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED ([DepartmentID])
);
GO

CREATE TABLE [dbo].[Instructors] (
    [InstructorID] INT           IDENTITY(1,1) NOT NULL,
    [LastName]     NVARCHAR(MAX) NOT NULL,
    [FirstName]    NVARCHAR(MAX) NOT NULL,
    [HireDate]     DATETIME2(7)  NOT NULL,
    CONSTRAINT [PK_Instructors] PRIMARY KEY CLUSTERED ([InstructorID])
);
GO

CREATE TABLE [dbo].[Students] (
    [StudentID]      INT           IDENTITY(1,1) NOT NULL,
    [LastName]       NVARCHAR(MAX) NOT NULL,
    [FirstName]      NVARCHAR(MAX) NOT NULL,
    [EnrollmentDate] DATETIME2(7)  NOT NULL,
    CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED ([StudentID])
);
GO

CREATE TABLE [dbo].[Courses] (
    [CourseID]     INT           IDENTITY(1,1) NOT NULL,
    [Title]        NVARCHAR(MAX) NOT NULL,
    [Credits]      INT           NOT NULL,
    [DepartmentID] INT           NOT NULL,
    CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED ([CourseID]),
    CONSTRAINT [FK_Courses_Departments_DepartmentID] FOREIGN KEY ([DepartmentID])
        REFERENCES [dbo].[Departments] ([DepartmentID]) ON DELETE CASCADE
);
GO
CREATE NONCLUSTERED INDEX [IX_Courses_DepartmentID] ON [dbo].[Courses] ([DepartmentID]);
GO

CREATE TABLE [dbo].[Enrollments] (
    [EnrollmentID] INT          IDENTITY(1,1) NOT NULL,
    [CourseID]     INT          NOT NULL,
    [StudentID]    INT          NOT NULL,
    [Grade]        DECIMAL(5,2) NOT NULL,
    CONSTRAINT [PK_Enrollments] PRIMARY KEY CLUSTERED ([EnrollmentID]),
    CONSTRAINT [FK_Enrollments_Courses_CourseID] FOREIGN KEY ([CourseID])
        REFERENCES [dbo].[Courses] ([CourseID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Enrollments_Students_StudentID] FOREIGN KEY ([StudentID])
        REFERENCES [dbo].[Students] ([StudentID]) ON DELETE CASCADE
);
GO
CREATE NONCLUSTERED INDEX [IX_Enrollments_CourseID]  ON [dbo].[Enrollments] ([CourseID]);
CREATE NONCLUSTERED INDEX [IX_Enrollments_StudentID] ON [dbo].[Enrollments] ([StudentID]);
GO

/* ---------- Sample data ---------- */
SET IDENTITY_INSERT [dbo].[Departments] ON;
INSERT INTO [dbo].[Departments] ([DepartmentID], [DepartmentName], [Budget], [StartDate]) VALUES
    (1, N'Computer Science',    100000.00, '2025-04-01T07:02:00'),
    (2, N'Information Systems', 200000.00, '2025-04-01T07:07:00');
SET IDENTITY_INSERT [dbo].[Departments] OFF;

SET IDENTITY_INSERT [dbo].[Instructors] ON;
INSERT INTO [dbo].[Instructors] ([InstructorID], [LastName], [FirstName], [HireDate]) VALUES
    (1, N'Pham', N'Trung', '2015-09-01T00:00:00'),
    (2, N'Nguyen', N'Lan', '2018-02-15T00:00:00');
SET IDENTITY_INSERT [dbo].[Instructors] OFF;

SET IDENTITY_INSERT [dbo].[Students] ON;
INSERT INTO [dbo].[Students] ([StudentID], [LastName], [FirstName], [EnrollmentDate]) VALUES
    (1, N'Tran', N'An',  '2024-09-05T00:00:00'),
    (2, N'Le',   N'Binh','2024-09-05T00:00:00'),
    (3, N'Vo',   N'Chi', '2025-02-10T00:00:00');
SET IDENTITY_INSERT [dbo].[Students] OFF;

SET IDENTITY_INSERT [dbo].[Courses] ON;
INSERT INTO [dbo].[Courses] ([CourseID], [Title], [Credits], [DepartmentID]) VALUES
    (1, N'Software Engineering',  3, 1),
    (2, N'Database Systems',      3, 2),
    (3, N'Web Application Design', 4, 1);
SET IDENTITY_INSERT [dbo].[Courses] OFF;

SET IDENTITY_INSERT [dbo].[Enrollments] ON;
INSERT INTO [dbo].[Enrollments] ([EnrollmentID], [CourseID], [StudentID], [Grade]) VALUES
    (1, 1, 1, 8.50),
    (2, 2, 1, 9.00),
    (3, 1, 2, 7.50),
    (4, 3, 3, 8.00);
SET IDENTITY_INSERT [dbo].[Enrollments] OFF;
GO

/* ---------- Quick check ---------- */
SELECT e.EnrollmentID, s.LastName + N' ' + s.FirstName AS Student, c.Title AS Course, d.DepartmentName, e.Grade
FROM Enrollments e
JOIN Students    s ON s.StudentID    = e.StudentID
JOIN Courses     c ON c.CourseID     = e.CourseID
JOIN Departments d ON d.DepartmentID = c.DepartmentID;
GO
