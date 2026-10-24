# 🎓 StudentAdoNet — Student Management System

A simple and responsive **Student Management System** built with ASP.NET Core MVC, C#, ADO.NET, and Microsoft SQL Server. This application allows users to manage student records through a clean web interface with complete CRUD functionality.

## ✨ Features

- 📋 **View Students** — Display all registered students in a structured table.
- ➕ **Add Student** — Register students with their first name, last name, email, and course.
- 🔍 **View Details** — View complete information for an individual student.
- ✏️ **Edit Student** — Update existing student information.
- 🗑️ **Delete Student** — Delete records with a confirmation page.
- ✅ **Form Validation** — Validate required fields, email format, and input lengths.
- 📧 **Duplicate Email Prevention** — Prevent duplicate email addresses.
- 🔐 **Parameterized SQL Queries** — Help protect against SQL injection.
- 💬 **User Feedback** — Display success messages after CRUD operations.
- 📱 **Responsive UI** — Bootstrap-based layout for different screen sizes.
- ⚡ **Asynchronous Database Operations** — Use async ADO.NET operations for database access.

## 🛠️ Tech Stack

| Technology | Purpose |
|---|---|
| C# | Backend programming |
| ASP.NET Core MVC | Web application framework |
| .NET 10 | Target framework |
| ADO.NET | Database connectivity and operations |
| Microsoft SQL Server Express | Relational database |
| Microsoft.Data.SqlClient | SQL Server data provider |
| HTML5 & CSS3 | Structure and styling |
| Bootstrap | Responsive UI |
| Razor Views | Dynamic HTML rendering |
| Git & GitHub | Version control and source code hosting |

## 🏗️ Project Structure

```text
StudentAdoNet/
├── Controllers/
│   └── StudentsController.cs
├── Data/
│   └── StudentRepository.cs
├── Models/
│   └── Student.cs
├── Views/
│   ├── Shared/
│   │   └── _Layout.cshtml
│   └── Students/
│       ├── Index.cshtml
│       ├── Create.cshtml
│       ├── Details.cshtml
│       ├── Edit.cshtml
│       └── Delete.cshtml
├── wwwroot/
│   └── css/
│       └── site.css
├── Properties/
├── Program.cs
├── appsettings.json
├── StudentAdoNet.csproj
└── README.md
```

## ⚙️ Prerequisites

Before running the project, make sure you have:

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [SQL Server Express](https://www.microsoft.com/sql-server/sql-server-downloads)
- [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms)
- Visual Studio, Visual Studio Code, or another compatible editor

## 🗄️ Database Setup

### 1. Create the database

Open SQL Server Management Studio and execute:

```sql
CREATE DATABASE StudentAdoNetDB;
GO

USE StudentAdoNetDB;
GO
```

### 2. Create the Students table

```sql
CREATE TABLE Students (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Course NVARCHAR(100) NOT NULL
);
GO
```

> If the database and table already exist, do not create them again.

### 3. Configure the connection string

Open `appsettings.json` and make sure the connection string points to your SQL Server instance and database.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=StudentAdoNetDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Replace `YOUR_SERVER_NAME` with your actual SQL Server instance name. Ensure that the connection-string key matches the one used in `StudentRepository.cs`.

**Security note:** The example uses Windows Authentication. Never commit database passwords, credentials, or other secrets to GitHub.

## 🚀 Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/ankitpasvan/StudentAdo.net.git
```

### 2. Navigate to the project directory

```bash
cd StudentAdo.net
```

If the project file is inside a subdirectory, navigate to the directory containing `StudentAdoNet.csproj`.

### 3. Restore dependencies

```bash
dotnet restore
```

### 4. Build the application

```bash
dotnet build
```

### 5. Run the application

```bash
dotnet run
```

Open the local URL displayed in the terminal. For example:

```text
http://localhost:5155
```

Navigate to `/Students` to access the student management page.

## 🧪 Testing Checklist

After starting the application, verify the following:

- [ ] The Students page loads successfully.
- [ ] A new student can be registered.
- [ ] Student records appear in the list.
- [ ] Student details can be viewed.
- [ ] Existing student information can be edited.
- [ ] Duplicate email addresses are rejected.
- [ ] Delete confirmation works correctly.
- [ ] Deleted records are removed from the database.
- [ ] Data persists after restarting the application.

You can verify database records using:

```sql
SELECT Id, FirstName, LastName, Email, Course
FROM dbo.Students;
```

## 🔒 Security and Validation

- SQL queries use parameters instead of concatenating user input.
- Server-side model validation checks submitted data.
- The database enforces unique email addresses.
- Delete operations use a POST request after confirmation.
- Windows Authentication can be used for local SQL Server access.

Keep NuGet dependencies updated and check for security advisories using:

```bash
dotnet list package --vulnerable --include-transitive
```

## 🎯 Project Objective

The objective of this project is to demonstrate practical knowledge of ASP.NET Core MVC, C#, ADO.NET, SQL Server connectivity, CRUD operations, input validation, and basic web application development.

## 👨‍💻 Author

**Ankit Pasvan**

GitHub: [@ankitpasvan](https://github.com/ankitpasvan)

## 📄 License

This project is intended for learning and educational purposes. Add a license file if you wish to distribute it under a specific open-source license.
