using System.Collections.Immutable;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UniversitySample.Students.Domain.Dto;
using UniversitySample.Students.Domain.Validations;
using UniversitySample.Students.Service;
using UniversitySample.Students.Service.DataAccess;
using UniversitySample.Students.Service.Mapping;
using UniversitySample.Students.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddStudentDb();
builder.Services.AddAutoMapper(typeof(StudentProfile).Assembly);
builder.Services.AddScoped<IValidator<StudentDetailsDto>, StudentValidator>();
builder.Services.AddTransient<StudentService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policyBuilder => policyBuilder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

var dbContextFactory = app.Services.GetRequiredService<IDbContextFactory<StudentDbContext>>();
var dbContext = dbContextFactory.CreateDbContext();
dbContext.Database.Migrate();

app.Run();
