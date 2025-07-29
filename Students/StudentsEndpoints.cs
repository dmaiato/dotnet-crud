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
      async (AddStudentRequest request, AppDbContext context, CancellationToken ct) =>
      {
        var exists = await context.Students.AnyAsync(
          student => student.Name == request.Name, ct
        );

        if (exists) return Results.Conflict("Student already exists.");

        var newStudent = new Student(request.Name);
        await context.Students.AddAsync(newStudent, ct); // adds new student to db, works like a list
        await context.SaveChangesAsync(ct); // only saves on db after this line

        var studentDTO = new StudentDTO(newStudent.Id, newStudent.Name);

        return Results.Ok(studentDTO);
      });

    // GET - all active students
    StudentsRoutes.MapGet("", async (AppDbContext context, CancellationToken ct) =>
    {
      var students = await context
              .Students
              .Where(student => student.Active) // list filter to sql query filter (very clever lol)
              .Select(student => new StudentDTO(student.Id, student.Name))
              .ToListAsync(ct);

      return students;
    });

    // PUT - update student name
    StudentsRoutes.MapPut("{id:guid}",
      async (Guid id, UpdateStudentRequest request, AppDbContext context, CancellationToken ct) =>
      {
        var student = await context.Students
          .SingleOrDefaultAsync(student => student.Id == id, ct);

        if (student == null) return Results.NotFound();

        student.UpdateName(request.Name);

        await context.SaveChangesAsync(ct);
        return Results.Ok(new StudentDTO(student.Id, student.Name));
      });

    // DELETE - soft deleting, as in deactivating student
    StudentsRoutes.MapDelete("{id:guid}",
      async (Guid id, AppDbContext context, CancellationToken ct) =>
      {
        var student = await context
          .Students.SingleOrDefaultAsync(student => student.Id == id, ct);

        if (student == null) return Results.NotFound();

        student.Deactivate();

        await context.SaveChangesAsync(ct);
        return Results.Ok();
      });
  }
}