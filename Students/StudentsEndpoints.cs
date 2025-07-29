using Microsoft.EntityFrameworkCore;
using SimpleAPI.Data;

namespace SimpleAPI.Students;

public static class StudentsEndpoints
{
  public static void AddStudentsEndpoints(this WebApplication app)
  {
    // route group simplification
    var StudentsRoutes = app.MapGroup("students");

    // POST - create new student
    StudentsRoutes.MapPost("",
      async (AddStudentRequest request, AppDbContext context) =>
    {
      var exists = await context.Students.AnyAsync(
        student => student.Name == request.Name
      );

      if (exists) return Results.Conflict("Student already exists.");

      var newStudent = new Student(request.Name);
      // adds new student to db, works like a list
      await context.Students.AddAsync(newStudent);
      // only saves on db after this line
      await context.SaveChangesAsync();

      return Results.Ok(newStudent);
    });

    // GET - all active students
    StudentsRoutes.MapGet("", async (AppDbContext context) =>
    {
      var students = await context
              .Students
              .Where(student => student.Active) // list filter to sql query filter (very clever lol)
              .ToListAsync();

      return students;
    });

    // PUT - update student name
    StudentsRoutes.MapPut("{id:guid}",
      async (Guid id, UpdateStudentRequest request, AppDbContext context) =>
    {
      var student = await context.Students
        .SingleOrDefaultAsync(student => student.Id == id);

      if (student == null) return Results.NotFound();

      student.UpdateName(request.Name);

      await context.SaveChangesAsync();
      return Results.Ok(student);
    });
  }
}