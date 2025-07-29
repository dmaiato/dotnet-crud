# SimpleAPI

![C#](https://img.shields.io/badge/C%23-9.0-239120?logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-9.0-blueviolet)
![SQLite](https://img.shields.io/badge/SQLite-3.0-blue?logo=sqlite&logoColor=white)

A minimal ASP.NET Core Web API for managing students, using Entity Framework Core and SQLite.

## Features

- Add new students
- List active students
- Update student names
- Soft-delete (deactivate) students

## Tech Stack

- ASP.NET Core (.NET 9)
- Entity Framework Core (with SQLite)
- Minimal API endpoints

## Getting Started

1. **Clone the repository**

   ```sh
   git clone https://github.com/yourusername/SimpleAPI.git
   cd SimpleAPI
   ```

2. **Restore dependencies**

   ```sh
   dotnet restore
   ```

3. **Run database migrations**

   ```sh
   dotnet ef database update
   ```

4. **Run the API**

   ```sh
   dotnet run
   ```

5. **Test**

   Use [SimpleAPI.http](SimpleAPI.http) or any HTTP client (like Postman) to interact with the API.

## Project Structure

- [`Program.cs`](Program.cs): App startup and configuration
- [`Data/AppDbContext.cs`](Data/AppDbContext.cs): EF Core database context
- [`Students/StudentsEndpoints.cs`](Students/StudentsEndpoints.cs): API endpoints for students
- [`Students/Student.cs`](Students/Student.cs): Student entity
- [`Students/StudentDTO.cs`](Students/StudentDTO.cs): Data transfer object for students
- [`Students/AddStudentRequest.cs`](Students/AddStudentRequest.cs): Request model for adding students
- [`Students/UpdateStudentRequest.cs`](Students/UpdateStudentRequest.cs): Request model for updating students
