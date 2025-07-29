namespace SimpleAPI.Students;

public static class StudentsEndpoints
{
  public static void AddStudentsEndpoints(this WebApplication app)
  {
    // app.MapGet("students", () => "Hello Students" );
    app.MapGet("students", () => new Student("David") );
  }
}