using WebApplication1;
using Microsoft.EntityFrameworkCore;
using CollegeManagementSystem;
using WebApplication1.Repositories.StudentRepository;
using WebApplication1.Repositories.CourseRepository;
using WebApplication1.Repositories.ModuleRepository;
using WebApplication1.Repositories.EnrollmentRepository;
using WebApplication1.Repositories.InstructorRepository;
using WebApplication1.Repositories.ModuleInstructorRepository;
using WebApplication1.Services.StudentServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("defaultConnection")));

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IModuleRepository, ModuleRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
builder.Services.AddScoped<IModuleInstructorRepository, ModuleInstructorRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
