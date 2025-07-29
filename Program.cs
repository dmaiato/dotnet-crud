using SimpleAPI.Data;
using SimpleAPI.Students;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// app.MapGet("Hello-World", () => "Hello World");
// StudentsEndpoints.AddStudentsEndpoints(app);

// methods above do not utilize 'this' in function

// route configuration
app.AddStudentsEndpoints();

app.Run();
